using System;
using System.Collections.Generic;
using System.Text;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class EmployeeJsonViewModel
    {
        public string Username { get; set; }

        public ICollection<TaskJsonViewModel> Tasks { get; set; }
    }

    public class TaskJsonViewModel
    {
        public string TaskName { get; set; }

        public string OpenDate { get; set; }

        public string DueDate { get; set; }

        public string LabelType { get; set; }

        public string ExecutionType { get; set; }
    }
}


/*[
  {
    "Username": "mmcellen1",
    "Tasks": [
      {
        "TaskName": "Pointed Gourd",
        "OpenDate": "10/08/2018",
        "DueDate": "10/24/2019",
        "LabelType": "Priority",
        "ExecutionType": "ProductBacklog"
      },
      {
        "TaskName": "Columbian",
        "OpenDate": "10/24/2018",
        "DueDate": "10/20/2019",
        "LabelType": "Hibernate",
        "ExecutionType": "InProgress"
      },
*/