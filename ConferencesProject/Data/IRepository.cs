using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.Models;

namespace ConferencesProject.Data
{
    public interface IRepository
    {
        Task AddUserToConfAsync(int idConf, string idUser);
        Task<ApplicationUser[]> GetAllUsersAsync();
        Task<UserConf[]> GetUsersByConfIdAsync(int id);
        void AddUserConf(string email, string id);
        void Save();
        void DeleteConference(Conference conference);
        Task<Conference[]> GetAllConferecesAsync();
        Task<Conference> GetConferenceAsync(int id);
        void AddConference(Conference conf);
        Task<Talk[]> GetAllTalksAsync();
        Task<Talk> GetTalkByTalkIdAsync(int id);
        Task<Talk[]> GetTalksByConfIdAsync(int id);
        void AddTalk(Talk talk);
        void DeleteTalk(Talk talk);
    }
}
