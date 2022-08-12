using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectXmlViewModel
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }
        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }
        [XmlArray("Tasks")]
        public TaskXmlViewModel[] Tasks { get; set; }
    }
    [XmlType("Task")]
    public class TaskXmlViewModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Label")]
        public string Label { get; set; }
    }
}


/*<Projects>
  <Project TasksCount="10">
    <ProjectName>Hyster-Yale</ProjectName>
    <HasEndDate>No</HasEndDate>
    <Tasks>
      <Task>
        <Name>Broadleaf</Name>
        <Label>JavaAdvanced</Label>
      </Task>
*/