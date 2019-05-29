using BAGCST.api.StudySystem.Models;

namespace BAGCST.api.StudySystem.Database
{
    public interface IStudyCourseDB
    {
        StudyCourse[] getAllCourses();

        StudyCourse getCourseById(int id);
    }
}
