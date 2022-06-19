using DATN_Back_end.Config;
using DATN_Back_end.Dto.DtoReport;
using DATN_Back_end.Exceptions;
using DATN_Back_end.Extensions;
using DATN_Back_end.Models;
using DATN_Back_end.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly DataContext dataContext;

        private readonly ICloudinaryService cloudinaryService;

        private readonly CloudinaryConfig cloudinaryConfig;

        public ReportRepository(DataContext dataContext,
            ICloudinaryService cloudinaryService,
            CloudinaryConfig cloudinaryConfig) : base(dataContext)
        {
            this.dataContext = dataContext;
            this.cloudinaryService = cloudinaryService;
            this.cloudinaryConfig = cloudinaryConfig;
        }

        public async Task Create(ReportFormDto reportFormDto)
        {
            reportFormDto.UploadFileLink = await CheckForUploading(reportFormDto.UploadFileLink);

            reportFormDto.CreatedTime = DateTime.Now;

            await base.Create(reportFormDto);
        }

        public async Task<List<ReportItem>> Get()
        {
            return await dataContext.Reports
                .Include(x => x.Comments)
                .Select(x => x.ConvertTo<ReportItem>())
                .ToListAsync();
        }

        public async Task<ReportDetail> Get(Guid id)
        {
            var entry = await dataContext.Reports
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Select(x => x.ConvertTo<ReportDetail>())
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                throw new NotFoundException("Report cannot be found");
            }

            return entry;
        }

        public async Task Update(Guid id, ReportFormDto reportFormDto)
        {
            reportFormDto.UploadFileLink = await CheckForUploading(reportFormDto.UploadFileLink);

            reportFormDto.UpdatedTime = DateTime.Now;

            await base.Update(id, reportFormDto);
        }

        private async Task<string> CheckForUploading(string urlOrBase64)
        {
            if (string.IsNullOrEmpty(urlOrBase64))
            {
                return string.Empty;
            }

            if (new Regex("http(s)?://").IsMatch(urlOrBase64))
            {
                return urlOrBase64;
            }

            var base64Header = "base64,";
            var imageHeader = "image/";
            var fileExtension = urlOrBase64.Substring(urlOrBase64.IndexOf(imageHeader) + imageHeader.Length, urlOrBase64.IndexOf(base64Header) - base64Header.Length);

            var imageName = Guid.NewGuid().ToString("N") + "." + fileExtension;
            var imageData = urlOrBase64.Substring(urlOrBase64.IndexOf(base64Header) + base64Header.Length);
            var fileData = Convert.FromBase64String(imageData);

            using var memoryStream = new MemoryStream(fileData);
            var img = Image.FromStream(memoryStream);

            var resizedImage = img.EnsureLimitSize(cloudinaryConfig.CoverLimitHeight, cloudinaryConfig.CoverLimitWidth);

            var dataUpload = resizedImage.ImageToByteArray();

            var uploadResult = await cloudinaryService.UploadImage(imageName, dataUpload);

            return uploadResult;
        }
    }
}
