namespace P01_StudentSystem.Data.Models
{
    /*(enum – can be Video, Presentation, Document or Other) когато създадем enuum класа по default започва с индекси 0, 1 ,2...
     Хубаво е да си ги индексираме самостоятелно като например 10,20,30,40 така напред във времето можем да добавим нещо в средата например
    между 10 и 20 добавяме 15*/
    public enum ResourceType
    {
        Video = 10,
        Presentation = 20,
        Document = 30,
        Other = 40
    }
}