# Kitchen Management Console App

Console app quan ly kitchen mon an, ap dung 2 design pattern:

- `Singleton`: `KitchenManager` dam bao chi co 1 doi tuong quan ly dish va order trong toan bo ung dung.
- `Factory`: `DishFactory` tao dung object mon an theo loai duoc chon thay vi khoi tao truc tiep trong menu.

## Chuc nang

- Xem danh sach mon an
- Them mon an moi
- Tao order gom nhieu mon
- Cap nhat trang thai order
- Xem dashboard thong ke trong bep

## Cau truc chinh

- `Program.cs`: diem vao cua ung dung
- `UI/ConsoleMenu.cs`: menu va nhap xuat console
- `Services/KitchenManager.cs`: singleton quan ly dish va order
- `Factories/DishFactory.cs`: factory tao dish
- `Models/*`: model mon an va order
- `Enums/*`: enum cho loai mon va trang thai order

## Cach chay

```bash
dotnet run
```

## Du lieu mau

Ung dung tu dong seed san cac mon:

- Spring Roll
- Beef Steak
- Orange Juice
- Cheesecake
