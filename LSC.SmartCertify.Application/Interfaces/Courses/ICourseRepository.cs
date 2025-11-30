using LSC.SmartCertify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSC.SmartCertify.Application.Interfaces.Courses
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCoursesAsync();

        Task<Course?> GetCourseByIdAsync(int courseId);

        Task<bool> IsTitleDuplicateAsync(string title);
        Task AddCourseAsync(Course course);

        Task UpdateCourseAsync(Course course);

        Task DeleteCourseAsync(Course course);

        Task UpdateDescriptionAsync(int courseId, string description);
    }
}
