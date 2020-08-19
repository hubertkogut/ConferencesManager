using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.Models;
using System.Data.Entity;
using Microsoft.Owin.Security;

namespace ConferencesProject.Data
{

    public class Repository : IRepository
    {
        private readonly ConferenceContext _context;
        private readonly ApplicationDbContext _apContext;

        public Repository(ConferenceContext context, ApplicationDbContext apContext)
        {
            _context = context;
            _apContext = apContext;
        }
        
        public async Task<ApplicationUser[]> GetAllUsersAsync()
        {
            var model = await _apContext.Users.ToArrayAsync();
            return model;
        }

        public void AddUserConf(string email, string id)
        {
            UserConf model = new UserConf();
            model.Email = email;
            model.IdInAspNetUsers = id;
            _context.UserConfs.Add(model);
            Save();
        }

        public async Task<UserConf[]> GetUsersByConfIdAsync(int id)
        {
            var query = await _context.Conferences
                .Where(c => c.Id == id)
                .SelectMany(c => c.ParticipatingUsers)
                .ToArrayAsync();

            return query;
        }


        public async Task AddUserToConfAsync(int idConf, string idUser)
        {
            var userConf = await _context.UserConfs.Where(c => c.IdInAspNetUsers == idUser).FirstOrDefaultAsync();
            var conf = await _context.Conferences.Where(c => c.Id == idConf).FirstOrDefaultAsync();
            conf.ParticipatingUsers.Add(userConf);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteConference(Conference conference)
        {
            if (conference.Talks != null)
            {
                foreach (var talk in conference.Talks.ToList())
                {
                    _context.Talks.Remove(talk);
                }
            }

            _context.Locations.Remove(conference.Location);
            _context.Conferences.Remove(conference);
        }

        public async Task<Conference[]> GetAllConferecesAsync()
        {
            IQueryable<Conference> query = _context.Conferences
                .Include(c => c.Location);

            return await query.ToArrayAsync();
        }

        private async Task<List<Conference>> GetConferencesListAsync()
        {
            IQueryable<Conference> query = _context.Conferences;
            return await query.ToListAsync();
        }

        public async Task<ConferenceWithUsersNumber[]> GetAllConferecesSortedByPopularityAsync() // ugly solution, but i fail to did this by LINQ
        {
            var confList = await GetConferencesListAsync();
            List<ConferenceWithUsersNumber> conferenceWithUsersNumbers = new List<ConferenceWithUsersNumber>();

            foreach (Conference conf in confList)
            {
                int id = conf.Id;
                var userNumberArray = await GetUsersByConfIdAsync(id);
                int userNumber = userNumberArray.Length;

                conferenceWithUsersNumbers.Add( new ConferenceWithUsersNumber()
                {
                    Id = id,
                    Name = conf.Name,
                    UsersNumber = userNumber
                });
            }

            var confWithUsersOrdered = conferenceWithUsersNumbers.OrderByDescending(c => c.UsersNumber);

            return confWithUsersOrdered.ToArray();
        }

        public async Task<Conference> GetConferenceAsync(int id)
        {
            IQueryable<Conference> query = _context.Conferences
                .Include(c => c.Location);

            query = query.Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public void AddConference(Conference conf)
        {
            _context.Conferences.Add(conf);
        }

        public async Task<Talk[]> GetAllTalksAsync()
        {
            IQueryable<Talk> query = _context.Talks;
            return await query.ToArrayAsync();
        }

        public async Task<Talk> GetTalkByTalkIdAsync(int id)
        {
            IQueryable<Talk> query = _context.Talks;
            query = query.Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Talk[]> GetTalksByConfIdAsync(int id)
        {
            IQueryable<Talk> query = _context.Talks;
            query = query
                .Where(t => t.Conference.Id == id)
                .OrderByDescending(t => t.Title);

            return await query.ToArrayAsync();
        }

        public void AddTalk(Talk talk)
        {
            _context.Talks.Add(talk);
        }

        public void DeleteTalk(Talk talk)
        {
            _context.Talks.Remove(talk);
        }
    }

}
