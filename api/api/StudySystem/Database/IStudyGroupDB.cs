using BAGCST.api.StudySystem.Models;

namespace BAGCST.api.StudySystem.Database
{
    public interface IStudyGroupDB
    {
        StudyGroup[] getAll();

        StudyGroup getByID(int id);
    }
}
