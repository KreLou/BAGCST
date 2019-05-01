using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    interface IRightsDB
    {
        /// <summary>
        /// search for Right by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Right|null</returns>
        Right getRight(int id);

        /// <summary>
        /// get all Rights
        /// </summary>
        /// <returns>Array of Rights with length>=0</returns>
        Right[] getAllRights();

        /// <summary>
        /// creates a Right
        /// </summary>
        /// <param name="item">Right</param>
        /// <returns>full Right</returns>
        Right createRight(Right item);

        /// <summary>
        /// edit Right
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited Right|null</returns>
        Right editRight(int id, Right item);

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
        Right getRightbyPath(string path);
    }
}
