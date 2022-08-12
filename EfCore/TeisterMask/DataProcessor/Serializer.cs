namespace TeisterMask.DataProcessor
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Data;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;
    using System.Collections.Generic;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            /*Export all projects that have at least one task.
             * For each project, export its name, tasks count, and if it has end (due) date which is represented like "Yes" and "No".
             * For each task, export its name and label type. Order the tasks by name (ascending). Order the projects by tasks count (descending),
             * then by name (ascending).
            NOTE: You may need to call .ToArray() function before the selection in order to detach entities
            from the database and avoid runtime errors (EF Core bug). 
            */

            var projectsXmlView = context.Projects
                .ToArray()
                .Where(x => x.Tasks.Count > 0)
                .Select(x => new ProjectXmlViewModel
                {
                    TasksCount = x.Tasks.Count,
                    ProjectName = x.Name,
                    HasEndDate = x.DueDate != null ? "Yes" : "No",
                    Tasks = x.Tasks.Select(t => new TaskXmlViewModel
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(o => o.Name)
                    .ToArray()
                })
                .OrderByDescending(o => o.TasksCount)
                .ThenBy(o => o.ProjectName)
                .ToArray();
            var result = XmlConverter.Serialize<ProjectXmlViewModel>(projectsXmlView, "Projects");
            return result;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            /*Select the top 10 employees who have at least one task that its open date is after or equal to the given date with their tasks
             * that meet the same requirement (to have their open date after or equal to the giver date). For each employee, export their username
             * and their tasks. For each task, export its name and open date (must be in format "d"), due date (must be in format "d"),
             * label and execution type. //////Order the tasks by due date (descending), then by name (ascending). Order the employees by all tasks 
             * (meeting above condition) count (descending), then by username (ascending).
            NOTE: Do not forget to use CultureInfo.InvariantCulture. You may need to call .ToArray()
            function before the selection in order to detach entities from the database and avoid runtime errors (EF Core bug).*/
            var employeeJsonView = context.Employees
                .Where(x => x.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .ToList()
                .Select(x => new EmployeeJsonViewModel
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks.Where(s => s.Task.OpenDate >= date)
                    .OrderByDescending(o => o.Task.DueDate)
                    .ThenBy(o => o.Task.Name)
                    .Select(t => new TaskJsonViewModel
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString()
                    })
                    .ToList()
                })
                .OrderByDescending(o => o.Tasks.Count())
                .ThenBy(o => o.Username)
                .Take(10)
                .ToList();
            var result = JsonConvert.SerializeObject(employeeJsonView, Formatting.Indented);
            return result;
        }
    }
}