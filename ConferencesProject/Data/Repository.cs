using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.Models;
using System.Data.Entity;

namespace ConferencesProject.Data
{

    public class Repository : IRepository
    {
        private readonly ConferenceContext _context;
        private readonly ApplicationDbContext _apContext;

        public Repository(ConferenceContext context)
        {
            _context = context;
            _apContext = new ApplicationDbContext();
        }

        //popraw
        public string GetUser()
        {
            var model = _apContext.Users.Local;
            var user = model.FirstOrDefault();
            var email = user.Email;
            return email;
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



        //Talks
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
