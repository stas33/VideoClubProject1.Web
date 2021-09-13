using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Core.Interfaces
{
    public interface ICopyService
    {
        int GetNumberOfAvailableCopies(int id);

        string GetAvailableCopyOfMovie(int id);

        PhysicalCopy GetFirstAvailableCopy(string query);

        PhysicalCopy GetLastAvailableCopyOfRental(History history);

        PhysicalCopy GetCopyByRental(History h);

        int GetLastAvailaibleCopyIdOfRental(History history);

        int GetLastAvailableCopyIdByMovieTitle(Movie m);

        PhysicalCopy GetCopyByCopyId(int id);

        void AddCopy(Movie movie);

    }
}
