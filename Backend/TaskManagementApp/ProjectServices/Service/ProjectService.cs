using ProjectServices.Model;
using ProjectServices.Repository;
using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Service
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Project Add(Project project)
        {
            return _projectRepository.Add(project);
        }

        public bool Delete(int id)
        {
            return _projectRepository.Delete(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public Project GetById(int id)
        {
            return _projectRepository.GetById(id);
        }

        public Project GetByName(string name)
        {
            return _projectRepository.GetByName(name);
        }

        public ProjectRequest Update(int id,ProjectRequest project)
        {
            return _projectRepository.Update(id, project);
        }
        public List<Sprint> GetSprintsByProjectId(int projectId)
        {
            return _projectRepository.GetSprintsByProjectId(projectId);
        }
        public bool EnrollUserToProject(int user_id, int project_id)
        {
            return _projectRepository.EnrollUserToProject(user_id, project_id);
            
        }

    }
}
