﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using DATN_Back_end.Models;
using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Extensions;
using DATN_Back_end.Config;
using System.IO;
using System.Drawing;
using DATN_Back_end.Services;

namespace DATN_Back_end.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext dataContext;

        private readonly ICloudinaryService cloudinaryService;

        private readonly CloudinaryConfig cloudinaryConfig;

        public UserRepository(DataContext dataContext, CloudinaryConfig cloudinaryConfig,
            ICloudinaryService cloudinaryService) : base(dataContext)
        {
            this.dataContext = dataContext;
            this.cloudinaryConfig = cloudinaryConfig;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task Create(UserFormCreate userForm)
        {
            userForm.Password = userForm.Password.Encrypt();

            await base.Create(userForm);
        }

        public async Task Update(Guid id, UserFormUpdate userForm)
        {
            userForm.AvatarUrl = await CheckForUploading(userForm.AvatarUrl);

            await base.Update(id, userForm);
        }

        public async Task<User> GetById(Guid Id)
        {
            return await dataContext.Users.FindAsync(Id);
        }

        public async Task<User> Get(string email)
        {
            return await dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
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
