namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var output = new StringBuilder();

            var xmlProjectsInsert = XmlConverter.Deserializer<ProjectXmlImportModel>(xmlString, "Projects");

            var validProjectsImports = new List<Project>();

            foreach (var currProject in xmlProjectsInsert)
            {
                var isValidOpenProjectDate = DateTime.TryParseExact(currProject.OpenDate,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDateProject);

                if (!IsValid(currProject) ||
                    !isValidOpenProjectDate)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validProject = new Project
                {
                    Name = currProject.Name,
                    OpenDate = openDateProject,
                    DueDate = DateTime.TryParseExact(currProject.DueDate, "dd / MM / yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime dueDateProject) ? (DateTime?)dueDateProject : null
                };

                var validTasks = new List<Task>();
                foreach (var currTask in currProject.Tasks)
                {
                    var isValidOpenDateTask = DateTime.TryParseExact(currTask.OpenDate, "dd/MM/yyyy",
                         CultureInfo.InvariantCulture,
                         DateTimeStyles.None,
                         out DateTime openDateTask);
                    var isValidDueDateTask = DateTime.TryParseExact(currTask.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime dueDateTask);
                    if (!IsValid(currTask) ||
                        !isValidOpenDateTask ||
                        !isValidDueDateTask)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (openDateTask < validProject.OpenDate ||
                        dueDateTask > validProject.DueDate)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    var validTask = new Task
                    {
                        Name = currTask.Name,
                        OpenDate = openDateTask,
                        DueDate = dueDateTask,
                        ExecutionType = Enum.Parse<ExecutionType>(currTask.ExecutionType),
                        LabelType = Enum.Parse<LabelType>(currTask.LabelType)
                    };
                    validTasks.Add(validTask);
                }
                validProject.Tasks = validTasks;

                validProjectsImports.Add(validProject);

                output.AppendLine(String.Format(SuccessfullyImportedProject, validProject.Name, validProject.Tasks.Count()));
            }
            context.Projects.AddRange(validProjectsImports);

            context.SaveChanges();

            return output.ToString().TrimEnd();
        }
        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var output = new StringBuilder();

            var jsonEmployeeImport = JsonConvert.DeserializeObject<EmployeeJsonImportModel[]>(jsonString);

            var validEmployees = new List<Employee>();

            foreach (var currEmployee in jsonEmployeeImport)
            {
                if (!IsValid(currEmployee))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validEmployee = new Employee
                {
                    Username = currEmployee.Username,
                    Email = currEmployee.Email,
                    Phone = currEmployee.Phone
                };

                var validIds = context.Tasks.Select(t => t.Id).ToList();

                foreach (var id in currEmployee.Tasks.Distinct())
                {

                    if (!validIds.Contains(id))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }
                    validEmployee.EmployeesTasks.Add(new EmployeeTask
                    {
                        TaskId = id
                    });
                }
                validEmployees.Add(validEmployee);

                output.AppendLine(String.Format(SuccessfullyImportedEmployee, currEmployee.Username, currEmployee.Tasks.Count()));
            }

            context.Employees.AddRange(validEmployees);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}