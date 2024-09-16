# Hướng dẫn dùng template daily quest
## 1. Tạo QuestData 
Là dữ liệu để QuestManager đọc và tạo quest.
### 1.1 Quest đơn
Click chuột phải vào folder muốn tạo quest chọn *create/ScriptableObject/Quest Data*.

Đặt tên và id: ***QuestData_0_QUESTNUMBER*** trong đó **QUESTNUMBER** không được trùng nhau.
### 1.2 Group Quest
Click chuột phải vào folder muốn tạo quest chọn *create/ScriptableObject/Quest Group Data*.

Đặt tên và id: ***QuestGroup_GROUPNUMBER*** trong đó **GROUPNUMBER** không được trùng nhau và bắt đầu từ 1.

Đặt tên các Quest Data và id: ***QuestData_GROUPNUMBER_QUESTNUMBER*** với **QUESTNUMBER** cần là các số liên tiếp.

Sau khi tạo đủ quest thì kéo thả **QuestData** vào **QuestGroup** mới tạo. Để quest có giá trị **QUESTNUMBER** nhỏ nhất ở đầu list 

### 1.3 List Daily Quest
Click chuột phải vào folder muốn tạo quest chọn *create/ScriptableObject/Quest Group Data*.

Kéo thả các GroupQuest và QuestData vào các list tương ứng.

## 2. Tạo QuestManager và gọi popup
Thêm script **QuestManager** vào game object chứa **UserData** (hoặc bất kỳ game object quản lý nào thuận tiện). Khai báo **QuestManager** trong script quản lý.


Kéo thả **ListDailyQuest** vào ô tương ứng trong **QuestManager**.


Gọi Popup: dùng hàm **PopupDailyMission.Show();**

## 3. Sprite Atlas cho questItem
Trong bảng hiển thị quest, cần tạo một spirte atlas để lưu trữ các biểu tượng của vật phẩm. Tên của các sprite cần khớp với các sprite được gọi tên trong các hàm **SetRewardIcon()** và **SetQuestIcon()**.

## 4. Cập nhật quest

