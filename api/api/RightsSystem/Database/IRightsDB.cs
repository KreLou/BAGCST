using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using BAGCST.api.RightsSystem.Models;

namespace BAGCST.api.RightsSystem.Database
{
    public interface IRightsDB
    {
        /// <summary>
        /// search for Right by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Right|null</returns>
        RightItem getRight(int id);

        /// <summary>
        /// get all Rights
        /// </summary>
        /// <returns>Array of Rights with length>=0</returns>
        RightItem[] getAllRights();

        /// <summary>
        /// creates a Right
        /// </summary>
        /// <param name="item">Right</param>
        /// <returns>full Right</returns>
        RightItem createRight(RightItem item);

        /// <summary>
        /// edit Right
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited Right|null</returns>
        RightItem editRight(int id, RightItem item);

        /// <summary>
        /// delete Right
        /// </summary>
        /// <param name="id">ID</param>
        void deleteRight(int id);

        /// <summary>
        /// Search for by given Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        RightItem getRightbyPath(string path);
    }
}
