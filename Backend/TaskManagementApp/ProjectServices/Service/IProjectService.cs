using ProjectServices.Model;
using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Service
{
    public interface IProjectService
    {
        public Project Add(Project project);

        public IEnumerable<Project> GetAll();

        public Project GetById(int id);

        public Project GetByName(string name);

        public ProjectRequest Update(int id,ProjectRequest project);

        public bool Delete(int id);
        public List<Sprint> GetSprintsByProjectId(int projectId);
        public bool EnrollUserToProject(int user_id, int project_id);

    }
}
