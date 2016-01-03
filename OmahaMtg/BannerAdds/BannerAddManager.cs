using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmahaMtg.Data;

namespace OmahaMtg.BannerAdds
{
    /// <summary>
    /// A banner add data manager. 
    /// </summary>
    /// <seealso cref="OmahaMtg.BannerAdds.IBannerAddManager" />
    public class BannerAddManager : IBannerAddManager
    {
        #region Private Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructors
        public BannerAddManager()
        {
            _context = new ApplicationDbContext();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a banner add.
        /// </summary>
        /// <param name="add">The banner add.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Banner add is invalid.</exception>
        public int CreateBannerAdd(BannerAdd add)
        {
            //Validate banner add.
            if(add == null)
            {
                throw new ArgumentException("Banner add is invalid.");
            }

            //Add and save to the database.
            _context.BannerAdds.Add(add);
            _context.SaveChanges();

            //Return the new id.
            return add.Id;
        }

        /// <summary>
        /// Gets a banner add from the data source.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Id is invalid.</exception>
        public BannerAdd GetBannerAdd(int id)
        {
            //Validate id.
            if (id < 0)
            {
                throw new ArgumentException("Id is invalid.");
            }

            //Return a banner add with the given id if it exists.
            return (from add
                    in _context.BannerAdds
                    where add.Id.Equals(id)
                    select add).FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of banner adds.
        /// </summary>
        /// <returns></returns>
        public BannerAdds GetBannerAdds()
        {
            List<BannerAdd> bannerAdds = _context.BannerAdds.ToList();
            BannerAdds listToReturn = new BannerAdds();
            foreach (var add in bannerAdds)
                listToReturn.Add(add);
            return listToReturn;
        }
        /// <summary>
        /// Updates a banner add.
        /// </summary>
        /// <param name="add">The banner add.</param>
        public void UpdateBannerAdd(BannerAdd add)
        {
            BannerAdd addToUpdate = (from a
                                     in _context.BannerAdds
                                     where a.Id.Equals(add.Id)
                                     select a).FirstOrDefault();
            if (!string.IsNullOrEmpty(add.FileName))
            {
                addToUpdate.FileName = add.FileName;
            }
            addToUpdate.Name = add.Name;
            addToUpdate.RotationEnd = add.RotationEnd;
            addToUpdate.RotationStart = add.RotationStart;

            _context.SaveChanges();
        }
        /// <summary>
        /// Deletes the banner add.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteBannerAdd(int id)
        {
            BannerAdd addToDelete = (from a
                                     in _context.BannerAdds
                                     where a.Id.Equals(id)
                                     select a).FirstOrDefault();
            _context.BannerAdds.Remove(addToDelete);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the active banner adds.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BannerAdds GetActiveBannerAdds()
        {
            BannerAdds bannerAddsList = new BannerAdds();
            var bannerAdds = (from a
                                     in _context.BannerAdds
                                     where a.RotationStart < DateTime.Now
                                     && a.RotationEnd > DateTime.Now
                                     && !a.FileName.Equals("none")
                                     select a).ToList();
            foreach (var add in bannerAdds)
            {
                bannerAddsList.Add(add);
            }
            return bannerAddsList;
        }

        /// <summary>
        /// Gets the random active banner add.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BannerAdd GetRandomActiveBannerAdd()
        {
            BannerAdds adds = GetActiveBannerAdds();
            if (adds.Count == 0)
                return null;
            int randomIndex = new Random().Next(adds.Count);
            return adds[randomIndex];
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
        #endregion
    }
}
