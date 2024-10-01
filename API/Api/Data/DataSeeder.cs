using Api.Models;

namespace Api.Data;

public static class DataSeeder
{
    public static void SeedData(ProductsDbContext db)
    {
        db.BoardEquipments.RemoveRange(db.BoardEquipments);
        db.Boards.RemoveRange(db.Boards);
        db.BoardTypes.RemoveRange(db.BoardTypes);
        db.Equipments.RemoveRange(db.Equipments);

        db.SaveChanges();

        List<Equipment> equipment = new List<Equipment>()
        {
            new Equipment { Name = "Fin" },
            new Equipment { Name = "Paddle" },
            new Equipment { Name = "Pump" },
            new Equipment { Name = "Leash" },
        };

        db.AddRange(equipment);

        List<BoardType> boardTypes = new List<BoardType>()
        {
            new BoardType { Name = "Shortboard" },
            new BoardType { Name = "Funboard" },
            new BoardType { Name = "Fish" },
            new BoardType { Name = "Longboard" },
            new BoardType { Name = "SUP" }
        };

        db.AddRange(boardTypes);
        db.SaveChanges(); 

        var shortboardType = db.BoardTypes.FirstOrDefault(bt => bt.Name == "Shortboard");
        var funboardType = db.BoardTypes.FirstOrDefault(bt => bt.Name == "Funboard");
        var fishType = db.BoardTypes.FirstOrDefault(bt => bt.Name == "Fish");
        var longboardType = db.BoardTypes.FirstOrDefault(bt => bt.Name == "Longboard");
        var supType = db.BoardTypes.FirstOrDefault(bt => bt.Name == "SUP");

        if (shortboardType == null || funboardType == null || fishType == null || longboardType == null || supType == null)
        {
            throw new Exception("One or more board types are missing.");
        }

        List<Board> boards = new List<Board>()
        {
            new Board { Name = "The Minilog", Length = 6.0, Width = 21.0, Thickness = 2.75, Volume = 38.8, BoardType = shortboardType, Price = 565.0, ImageUrl = "s326152794241300969_p345_i3_w5000.webp" },
            new Board { Name = "The Wide Glider", Length = 7.1, Width = 21.75, Thickness = 2.75, Volume = 44.16, BoardType = funboardType, Price = 685.0, ImageUrl = "s326152794241300969_p327_i17_w1168.png" },
            new Board { Name = "The Golden Ratio", Length = 6.3, Width = 21.85, Thickness = 2.9, Volume = 43.22, BoardType = funboardType, Price = 695.0, ImageUrl = "s326152794241300969_p327_i17_w1168.png" },
            new Board { Name = "Mahi Mahi", Length = 5.4, Width = 20.75, Thickness = 2.3, Volume = 29.39, BoardType = fishType, Price = 645.0, ImageUrl = "s326152794241300969_p332_i22_w1116.png" },
            new Board { Name = "The Emerald Glider", Length = 9.2, Width = 22.8, Thickness = 2.8, Volume = 65.4, BoardType = longboardType, Price = 895.0, ImageUrl = "s326152794241300969_p336_i55_w5000.webp" },
            new Board { Name = "The Bomb", Length = 5.5, Width = 21.0, Thickness = 2.5, Volume = 33.7, BoardType = shortboardType, Price = 645.0, ImageUrl = "s326152794241300969_p6_i6_w741.jpeg" },
            new Board { Name = "Walden Magic", Length = 9.6, Width = 19.4, Thickness = 3.0, Volume = 80.0, BoardType = longboardType, Price = 1025.0, ImageUrl = "s326152794241300969_p285_i27_w333.jpeg" },
            new Board { Name = "Naish One", Length = 12.6, Width = 30.0, Thickness = 6.0, Volume = 301.0, BoardType = supType, Price = 854.0, ImageUrl = "naish-nalu-10-6-s26-inflatable-sup.jpg" },
            new Board { Name = "Six Tourer", Length = 11.6, Width = 32.0, Thickness = 6.0, Volume = 270.0, BoardType = supType, Price = 611.0, ImageUrl = "stx-tourer-11-6-2022-2023-inflatable-sup-package.jpg" },
            new Board { Name = "Naish Maliko", Length = 14.0, Width = 25.0, Thickness = 6.0, Volume = 330.0, BoardType = supType, Price = 1304.0, ImageUrl = "naish-maliko-carbon-14-x-27-2024-inflatable-sup.jpg" }
        };

        db.Boards.AddRange(boards);
        db.SaveChanges();

        var naishOneBoard = db.Boards.FirstOrDefault(b => b.Name == "Naish One");
        var sixTourerBoard = db.Boards.FirstOrDefault(b => b.Name == "Six Tourer");
        var naishMalikoBoard = db.Boards.FirstOrDefault(b => b.Name == "Naish Maliko");

        var paddle = db.Equipments.FirstOrDefault(e => e.Name == "Paddle");
        var fin = db.Equipments.FirstOrDefault(e => e.Name == "Fin");
        var pump = db.Equipments.FirstOrDefault(e => e.Name == "Pump");
        var leash = db.Equipments.FirstOrDefault(e => e.Name == "Leash");

        if (naishOneBoard == null || sixTourerBoard == null || naishMalikoBoard == null || paddle == null || fin == null || pump == null || leash == null)
        {
            throw new Exception("One or more boards or equipment are missing.");
        }

        List<BoardEquipment> boardEquipments = new List<BoardEquipment>()
        {
            new BoardEquipment { Board = naishOneBoard, Equipment = paddle },
            new BoardEquipment { Board = sixTourerBoard, Equipment = fin },
            new BoardEquipment { Board = sixTourerBoard, Equipment = paddle },
            new BoardEquipment { Board = sixTourerBoard, Equipment = pump },
            new BoardEquipment { Board = sixTourerBoard, Equipment = leash },
            new BoardEquipment { Board = naishMalikoBoard, Equipment = fin },
            new BoardEquipment { Board = naishMalikoBoard, Equipment = paddle },
            new BoardEquipment { Board = naishMalikoBoard, Equipment = pump },
            new BoardEquipment { Board = naishMalikoBoard, Equipment = leash }
        };

        db.BoardEquipments.AddRange(boardEquipments);
        db.SaveChanges();
    }
}