﻿using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    public class IssueController : Controller
    {
        //TODO user a simple factory 
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        private ContributionRepository contributionRepository;
        private IssueRepository issueRepository;
        private DatabaseContext dbContext = new DatabaseContext();

        public IssueController()
        {
            userRepository = new UserRepository(dbContext);
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);
            issueRepository = new IssueRepository(dbContext);
        }
        // GET: liste of  Issues by project id
        public ActionResult Index(int id)
        {
            return View(projectRepository.FindById(id));
        }

        public ActionResult Add(int id)
        {
            AddIssueViewModel addIssueVM = new AddIssueViewModel(id);
            return View(addIssueVM);
        }

        [HttpPost]
        public ActionResult Add(AddIssueViewModel model)
        {
            var userName = this.ControllerContext.HttpContext.User.Identity.Name;
            var user = userRepository.FindByEmail(userName);
            ProjectEntity project = projectRepository.FindById(model.IdProject);
            if (!ModelState.IsValid)
            {
                model.IdProject = project.Id;
                return View(model);
            }
            IssueEntity issue = new IssueEntity
            {
                CreateAt= DateTime.Now,               
                EditAt  = DateTime.Now,
                Deleted = false,
                Project = project,
                Description = model.Description,
                RelatedArea = model.RelatedArea,
                Status      = model.Status,
            };

            issueRepository.Add(issue);
            project.Issues.Add(issue);

            return RedirectToAction("Index", "Issue", new { id = project.Id });
        }
    }
}