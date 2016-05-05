# QuickPlot
Tiện ích in ấn nhanh trong AutoCAD - copyright 2016 by Enesy
## Chức năng chính
* Kiểm tra một số lỗi cơ bản trước khi in ???
* In nhanh bản vẽ
* In nhanh một thư mục bản vẽ
* Tạo SheetSet Manager cho nhiều bản vẽ, bất kể là bản vẽ trên model hay layout, mỗi layout chứa một hay nhiều bản vẽ
* Lưu lại file in ấn với định dạng *.XML để lần sau có thể in lại một cách nhanh chóng
* Tạo file PDF nhanh chóng
* Chức năng Archive (thu gom) các bản vẽ được in vào 1 thư mục ???
* Thiết lập nét in nhanh.

## Description
### Kiểm tra một số lỗi cơ bản khi in
Chú giải cho chức năng này
### In nhanh bản vẽ
Chú giải cho chức năng này
### In nhanh một thư mục
Chú giải cho chức năng này
### Thiết lập nét in nhanh
**Mục tiêu của chức năng này**
* Cho phép người dùng chọn 1 loại đối tượng trên bản vẽ và gán bề dày nét in thông qua đối tượng này
* Nếu 2 màu trùng nhau thì phần mềm sẽ nhắc người dùng thiết lập lại
* Có thể nghĩ thêm một số phương thức khác để thiết lập nét in ??

# Nguyên tắc
Giao diện người dùng càng đơn giản càng tốt
#Cấu trúc thư mục
##CADAutomation
Thư mục này dùng để tham khảo code tương tác out process với AutoCAD
##src
Thư mục chứa mã của QuickPrint
###AutoCAD
Tất cả code thao tác với AutoCAD sẽ được đặt ở trong thư mục này
###CTB
Tất cả code thao tác với file *.ctb sẽ được đặt ở đây
###IO
Tất cả code thao tác I/O sẽ được đặt ở đây
###Security
Tất cả code bảo mật chương trình ở đây
#Actions in a galance
*001: Khi double click chuột vào 1 item trên list, chương trình tự động kiểm tra xem AuoCAD đã được mở chưa, sau đó mở bản vẽ tương ứng và gọi phương thức Print Preview của bản vẽ
