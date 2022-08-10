using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Theatre.Data.Models.Enums;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Play")]
    public class PlayViewModel
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Duration")]
        public string Duration { get; set; }
        [XmlAttribute("Rating")]
        public string Rating { get; set; }
        [XmlAttribute("Genre")]
        public string Genre { get; set; }
        [XmlArray("Actors")]
        public ActorViewXmlModel[] Actors { get; set; }
    }
    [XmlType("Actor")]
    public class ActorViewXmlModel
    {
        [XmlAttribute("FullName")]
        public string FullName { get; set; }
        [XmlAttribute("MainCharacter")]
        public string MainCharacter { get; set; }
    }
}


/*
 <Plays>
  <Play Title="A Raisin in the Sun" Duration="01:40:00" Rating="5.4" Genre="Drama">
    <Actors>
      <Actor FullName="Sylvia Felipe" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Sella Mains" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Sela Hillett" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Rodney O'Neill" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Robbert Tuvey" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Reamonn Maleby" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Loutitia Joy" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Irving Houlridge" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Cristine Van Brug" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Clerissa Fellgate" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
      <Actor FullName="Caye Blacklawe" MainCharacter="Plays main character in 'A Raisin in the Sun'." />
    </Actors>
*/
