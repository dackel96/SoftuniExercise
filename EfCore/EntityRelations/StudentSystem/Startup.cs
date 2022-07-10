using P01_StudentSystem.Data;
using System;

namespace P01_StudentSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            StudentSystemContext context = new StudentSystemContext();
            context.Database.EnsureDeleted();        // Това Лупва цикъла и всеки път ще създаваме свежа база (за по ясни тестове)
            context.Database.EnsureCreated();        // Това Лупва цикъла и всеки път ще създаваме свежа база (за по ясни тестове)
        }
    }
}
