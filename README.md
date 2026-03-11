# Kitchen Management Console App

Console app quan ly kitchen mon an duoc lam gon lai de de thuyet trinh va van giu 2 design pattern:

- `Singleton`: `KitchenManager` dam bao chi co 1 doi tuong quan ly dish va order trong toan bo ung dung.
- `Factory`: `DishFactory` tao `FoodDish` hoac `DrinkDish` tuy theo loai mon.

## Chuc nang

- Xem danh sach mon an
- Them mon an moi
- Tao order cho 1 mon
- Xem danh sach order
- Cap nhat trang thai order

## Cau truc chinh

- `Program.cs`: diem vao cua ung dung
- `UI/ConsoleMenu.cs`: menu va nhap xuat console
- `Services/KitchenManager.cs`: singleton quan ly dish va order
- `Factories/DishFactory.cs`: factory tao dish
- `Models/Dish.cs`: lop truu tuong cho mon an
- `Models/FoodDish.cs`: mon an loai food
- `Models/DrinkDish.cs`: mon an loai drink
- `Models/KitchenOrder.cs`: order don gian chi chua 1 mon
- `Enums/*`: enum cho loai mon va trang thai order

## Cach chay

```bash
dotnet run
```

## Du lieu mau

Ung dung tu dong seed san cac mon:

- Fried Rice
- Lemon Tea
