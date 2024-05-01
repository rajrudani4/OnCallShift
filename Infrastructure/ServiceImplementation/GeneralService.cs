using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context;
using ChatApp.Hubs;
using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System;
using ChatApp.Context.EntityClasses;
using ChatApp.Models.GeneralMessages;
using ChatApp.Business.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.ServiceImplementation
{
    public class GeneralService : IGeneralService
    {
        private readonly ChatAppContext _context;

        public GeneralService(ChatAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Areas> GetAreasList()
        {
            try
            {
                var areas = _context.Areas.ToList();

                return areas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GeneralMessageDTO> createPost(int userId, CreatePostModel postModel) {

            try
            {
                GeneralMessages message = null;
                message = new GeneralMessages
                {
                    Id = postModel.PostId,
                    AreaId = postModel.AreaCode,
                    Desc = postModel.Desc,
                    MessageFrom = userId,
                    Role = postModel.Role,
                    PayPerHour = postModel.PayPerHour,
                };

                    //add profile to db
                if(postModel.PostId == 0)
                {
                    _context.GeneralMessages.Add(message);
                }
                else
                {
                    _context.GeneralMessages.Update(message);
                }

                 _context.SaveChanges();

                IQueryable<GeneralMessages> ChatList = _context.GeneralMessages
                        .Where(e => e.MessageFrom == userId)
                        .OrderBy(e => e.CreatedAt)
                        .Include("MessageFromUser")
                        .Include("AreaFK");
                

                IList<GeneralMessageDTO> returnObj = new List<GeneralMessageDTO>();
                foreach (var GeneralMessage in ChatList)
                {
                    var newObj = ConvertGMToGMDTOModel(GeneralMessage);
                    returnObj.Add(newObj);
                }

                return returnObj;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GeneralMessageDTO> getAllMessages(int? AreaId, Profile user)
        {
            try
            {
                var ChatList = _context.GeneralMessages
                    .Where(e => e.MessageFrom != user.Id)
                    .OrderBy(e => e.CreatedAt)
                    .Include("MessageFromUser")
                    .Include("AreaFK");

                if (AreaId != 0)
                {
                    ChatList = ChatList.Where(e => e.AreaId == AreaId);
                }
                IList<GeneralMessageDTO> returnObj = new List<GeneralMessageDTO>();
                foreach (var GeneralMessage in ChatList)
                {
                    var newObj = ConvertGMToGMDTOModel(GeneralMessage);
                    returnObj.Add(newObj);
                }

                return returnObj;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<GeneralMessageDTO> getMyPosts(Profile user)
        {
            try
            {
                var ChatList = _context.GeneralMessages
                    .Where(e => e.MessageFrom == user.Id)
                    .OrderBy(e => e.CreatedAt)
                    .Include("MessageFromUser")
                    .Include("AreaFK");

                IList<GeneralMessageDTO> returnObj = new List<GeneralMessageDTO>();
                foreach (var GeneralMessage in ChatList)
                {
                    var newObj = ConvertGMToGMDTOModel(GeneralMessage);
                    returnObj.Add(newObj);
                }

                return returnObj;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private GeneralMessageDTO ConvertGMToGMDTOModel(GeneralMessages gm)
        {
            var returnObj = new GeneralMessageDTO()
            {
                Id = gm.Id,
                AreaId = gm.AreaFK.Id,
                AreaName = gm.AreaFK.Name,
                Role = gm.Role,
                Desc = gm.Desc,
                PayPerHour = gm.PayPerHour,
                CreatedAt = gm.CreatedAt,
                MessageFromUsername = gm.MessageFromUser.UserName,
                MessageFromUrl = gm.MessageFromUser.ImageUrl
            };

            return returnObj;
        }
    }
}
