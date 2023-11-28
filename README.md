A .net web api application for performing CRUD operations on pictures. You can also perform CRUD operations on albums and add/delete images to/from these albums. You can't perform operations on pictures and albums that aren't yours unless you are an Admin. Pictures and albums also have privacy settings, that prohibit you from accessing them if you aren't the user who uploaded it or an admin.

How to run it:
```bash
git clone https://github.com/mrajtar/ImageRed.git
```
Navigate into the directory:
```bawsh
cd ImageRed
```
Build the solution and then run it
```bash
dotnet build
```
```bash
dotnet run
```
## PICTURES
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/pictures`                             | Retrieve all public pictures.                      |
| `POST`   | `/api/pictures`                             | Create a new picture.                       |
| `GET`    | `/api/pictures/4`                          | Retrieve picture #4.                       |
| `PUT`  | `/api/pictures/4`                          | Update data in picture #4.                 |
| `DELETE` | `/api/pictures/4`                       | Delete picture #4.                    |
## ALBUMS
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/albums/8`                          | Retrieve album #8.                       |
| `POST`   | `/api/albums`                             | Create a new album.                       |
| `PUT`  | `/api/albums/8`                          | Update data in album #8.                 |
| `DELETE` | `/api/albums/8`                       | Delete album #8.                    |
| `POST` | `/api/albums/8/pictures/4`                       | Add picture #4 to album #8                   |
| `DELETE` | `/api/albums/8/pictures/4`                       | Delete picture #4 from album #8.                    |
## AUTH
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `POST`    | `/api/Auth/Register`                          | Register a user                       |
| `POST`   | `/api/Auth/Login`                             | Log in to a user                    |
