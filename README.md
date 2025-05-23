## **Actor + Use Cases**

ĐỎ: optional

### 1. Khách hàng (chưa đăng ký):

-   Tìm kiếm phòng trọ theo vị trí, giá, tiện nghi

-   Xem chi tiết phòng (hình ảnh, mô tả, giá, liên hệ)

-   Xem đánh giá của người thuê trước

-   Lưu phòng yêu thích (tạm thời qua cookie/session)

-   Tìm người ghép trọ chung

-   Đăng ký tài khoản

### 2. Khách hàng (đã đăng ký):

-   Đặt lịch hẹn xem phòng

-   Chat/nhắn tin với chủ trọ hoặc tư vấn viên

-   Đăng ký thuê phòng (gửi yêu cầu)

-   Theo dõi trạng thái yêu cầu thuê

-   Đánh giá phòng trọ sau khi ở

-   Lưu trữ lịch sử thuê/phòng yêu thích

-   Nhận thông báo khuyến mãi/phòng mới phù hợp

### 3. Chủ trọ:

-   Đăng bài cho thuê phòng (hình ảnh, mô tả, giá, vị trí, tiện nghi...)

-   Quản lý phòng trọ đã đăng (chỉnh sửa, ẩn/hiện, xoá)

-   Xem yêu cầu thuê/phản hồi từ khách

-   Chat với khách hàng

-   Xem thống kê lượt xem/phản hồi

-   Quản lý hợp đồng thuê (nếu có chức năng này)

### 4. Quản lý hệ thống (Admin):

-   Duyệt bài đăng phòng (nếu cần kiểm duyệt)

-   Quản lý người dùng (khách hàng, chủ trọ, tư vấn viên)

-   Quản lý báo cáo vi phạm, khiếu nại

-   Quản lý danh mục (tiện nghi, khu vực...)

-   Thống kê tổng quan (số lượng phòng, người dùng, hoạt động)

-   Gửi thông báo hệ thống

### 5. Tư vấn viên (nếu có):

-   Hỗ trợ khách hàng qua chat hoặc cuộc hẹn tư vấn

-   Gợi ý phòng phù hợp nhu cầu

## DB schema

**1. Users & Roles**

Users (

id UUID PK,

name TEXT,

email TEXT UNIQUE,

password_hash TEXT,

phone TEXT,

avatar_url TEXT,

role ENUM(\'guest\', \'registered_customer\', \'host\', \'admin\',
\'consultant\'),

created_at TIMESTAMP,

updated_at TIMESTAMP

)

**2. Buildings & Rooms**

Buildings (

id UUID PK,

host_id UUID FK -\> Users(id),

name TEXT,

address TEXT,

description TEXT,

location TEXT,

created_at TIMESTAMP,

updated_at TIMESTAMP

)

Rooms (

id UUID PK,

building_id UUID FK -\> Buildings(id),

name TEXT, \-- ví dụ: \"Phòng 101\"

description TEXT,

price DECIMAL,

status ENUM(\'active\', \'inactive\', \'hidden\'),

created_at TIMESTAMP,

updated_at TIMESTAMP

)

RoomImages (

id UUID PK,

room_id UUID FK -\> Rooms(id),

image_url TEXT

)

**3. Tiện nghi phòng**

Amenities (

id UUID PK,

name TEXT

)

RoomAmenities (

room_id UUID FK -\> Rooms(id),

amenity_id UUID FK -\> Amenities(id),

PRIMARY KEY(room_id, amenity_id)

)

**4. Favorites (yêu thích phòng)**

Favorites (

id UUID PK,

user_id UUID FK -\> Users(id),

room_id UUID FK -\> Rooms(id),

created_at TIMESTAMP

)

**5. Lịch hẹn xem phòng**

ViewAppointments (

id UUID PK,

user_id UUID FK -\> Users(id),

room_id UUID FK -\> Rooms(id),

appointment_time TIMESTAMP,

status ENUM(\'pending\', \'confirmed\', \'cancelled\'),

created_at TIMESTAMP

)

**6. Yêu cầu thuê phòng**

BookingRequests (

id UUID PK,

user_id UUID FK -\> Users(id),

room_id UUID FK -\> Rooms(id),

message TEXT,

status ENUM(\'pending\', \'approved\', \'rejected\', \'cancelled\'),

created_at TIMESTAMP,

updated_at TIMESTAMP

)

**7. Hợp đồng thuê (nếu có)**

Contracts (

id UUID PK,

booking_request_id UUID FK -\> BookingRequests(id),

start_date DATE,

end_date DATE,

contract_file_url TEXT,

created_at TIMESTAMP

)

**8. Chat & Tin nhắn**

ChatRooms (

id UUID PK,

user1_id UUID FK -\> Users(id),

user2_id UUID FK -\> Users(id),

created_at TIMESTAMP

)

Messages (

id UUID PK,

chat_room_id UUID FK -\> ChatRooms(id),

sender_id UUID FK -\> Users(id),

content TEXT,

sent_at TIMESTAMP

)

**9. Đánh giá phòng**

Reviews (

id UUID PK,

room_id UUID FK -\> Rooms(id),

user_id UUID FK -\> Users(id),

rating INT CHECK(rating BETWEEN 1 AND 5),

comment TEXT,

created_at TIMESTAMP

)

**10. Thông báo**

Notifications (

id UUID PK,

user_id UUID FK -\> Users(id),

title TEXT,

message TEXT,

is_read BOOLEAN DEFAULT FALSE,

created_at TIMESTAMP

)

**11. Báo cáo vi phạm / khiếu nại**

Reports (

id UUID PK,

reported_by UUID FK -\> Users(id),

reported_user UUID FK -\> Users(id),

reason TEXT,

status ENUM(\'open\', \'resolved\'),

created_at TIMESTAMP

)

**12. Danh mục dùng chung (tiện nghi, khu vực...)**

Categories (

id UUID PK,

name TEXT,

type ENUM(\'amenity\', \'location\')

)

## Entities

**User (Base class)**

> class User {
>
> id: UUID
>
> name: string
>
> email: string
>
> passwordHash: string
>
> phone: string
>
> avatarUrl: string
>
> role: \'guest\' \| \'registered_customer\' \| \'host\' \| \'admin\' \|
> \'consultant\'
>
> createdAt: Date
>
> updatedAt: Date
>
> login()
>
> updateProfile()
>
> }
>
> **Customer extends User**
>
> class Customer extends User {
>
> favorites: Room\[\]
>
> bookings: BookingRequest\[\]
>
> viewAppointments: ViewAppointment\[\]
>
> reviews: Review\[\]
>
> notifications: Notification\[\]
>
> searchRooms(filters): Room\[\]
>
> viewRoomDetails(roomId): Room
>
> addFavorite(roomId): void
>
> removeFavorite(roomId): void
>
> createBookingRequest(roomId, message): BookingRequest
>
> createViewAppointment(roomId, time): ViewAppointment
>
> chatWith(userId): ChatRoom
>
> reviewRoom(roomId, rating, comment): Review
>
> }
>
> **Host extends User**
>
> class Host extends User {
>
> buildings: Building\[\]
>
> postRoom(buildingId, roomData): Room
>
> updateRoom(roomId, data): void
>
> toggleRoomVisibility(roomId): void
>
> deleteRoom(roomId): void
>
> respondToBooking(requestId, action): void
>
> viewStatistics(): any
>
> manageContract(bookingId, data): Contract
>
> }
>
> **Admin extends User**
>
> class Admin extends User {
>
> approveRoomPost(roomId): void
>
> manageUsers(): User\[\]
>
> handleReports(): Report\[\]
>
> manageCategories(): Category\[\]
>
> sendSystemNotification(notification): void
>
> viewDashboard(): any
>
> }
>
> **Consultant extends User**
>
> class Consultant extends User {
>
> assistCustomer(customerId): void
>
> suggestRooms(customerId, criteria): Room\[\]
>
> }
>
> **Building**
>
> class Building {
>
> id: UUID
>
> hostId: UUID
>
> name: string
>
> address: string
>
> description: string
>
> location: string
>
> createdAt: Date
>
> updatedAt: Date
>
> getRooms(): Room\[\]
>
> }
>
> **Room**
>
> class Room {
>
> id: UUID
>
> buildingId: UUID
>
> name: string
>
> description: string
>
> price: number
>
> status: \'active\' \| \'inactive\' \| \'hidden\'
>
> images: RoomImage\[\]
>
> amenities: Amenity\[\]
>
> createdAt: Date
>
> updatedAt: Date
>
> getReviews(): Review\[\]
>
> }
>
> **Amenity**
>
> class Amenity {
>
> id: UUID
>
> name: string
>
> }
>
> **RoomImage**
>
> class RoomImage {
>
> id: UUID
>
> roomId: UUID
>
> imageUrl: string
>
> }
>
> **Favorite**
>
> class Favorite {
>
> id: UUID
>
> userId: UUID
>
> roomId: UUID
>
> createdAt: Date
>
> }
>
> **ViewAppointment**
>
> class ViewAppointment {
>
> id: UUID
>
> userId: UUID
>
> roomId: UUID
>
> appointmentTime: Date
>
> status: \'pending\' \| \'confirmed\' \| \'cancelled\'
>
> createdAt: Date
>
> }
>
> **BookingRequest**
>
> class BookingRequest {
>
> id: UUID
>
> userId: UUID
>
> roomId: UUID
>
> message: string
>
> status: \'pending\' \| \'approved\' \| \'rejected\' \| \'cancelled\'
>
> createdAt: Date
>
> updatedAt: Date
>
> }
>
> **Contract**
>
> class Contract {
>
> id: UUID
>
> bookingRequestId: UUID
>
> startDate: Date
>
> endDate: Date
>
> contractFileUrl: string
>
> createdAt: Date
>
> }
>
> **ChatRoom**
>
> class ChatRoom {
>
> id: UUID
>
> user1Id: UUID
>
> user2Id: UUID
>
> createdAt: Date
>
> sendMessage(senderId: UUID, content: string): Message
>
> getMessages(): Message\[\]
>
> }
>
> **Message**
>
> class Message {
>
> id: UUID
>
> chatRoomId: UUID
>
> senderId: UUID
>
> content: string
>
> sentAt: Date
>
> }
>
> **Review**
>
> class Review {
>
> id: UUID
>
> roomId: UUID
>
> userId: UUID
>
> rating: number // 1-5
>
> comment: string
>
> createdAt: Date
>
> }
>
> **Notification**
>
> class Notification {
>
> id: UUID
>
> userId: UUID
>
> title: string
>
> message: string
>
> isRead: boolean
>
> createdAt: Date
>
> markAsRead(): void
>
> }
>
> **Report**
>
> class Report {
>
> id: UUID
>
> reportedBy: UUID
>
> reportedUser: UUID
>
> reason: string
>
> status: \'open\' \| \'resolved\'
>
> createdAt: Date
>
> resolve(): void
>
> }
>
> **Category**
>
> class Category {
>
> id: UUID
>
> name: string
>
> type: \'amenity\' \| \'location\'
>
> }

## MVP

> **🎯 Mục tiêu MVP (Minimum Viable Product):**

**Tìm - Xem - Đăng ký - Quản lý cơ bản**

> **✅ Chức năng MVP nên gồm:**

#### **1. Guest (chưa đăng ký):**

-   Tìm kiếm phòng theo vị trí, giá

-   Xem chi tiết phòng (ảnh, mô tả, giá)

-   Đăng ký tài khoản

#### **2. Registered Customer:**

-   Đăng nhập / Đăng xuất

-   Đặt lịch hẹn xem phòng

-   Gửi yêu cầu thuê phòng

-   Xem trạng thái yêu cầu

#### **3. Host (chủ trọ):**

-   Đăng bài cho thuê phòng (thêm ảnh, mô tả, tiện nghi)

-   Quản lý bài đăng (ẩn/hiện/xoá)

#### **4. Admin (rất cơ bản):**

-   Duyệt bài đăng phòng

> **⚙️ Công nghệ:**

-   ASP.NET MVC (Views + Controller + Models)

-   Entity Framework Core (Code First)

-   SQL Server

-   Razor Views (UI đơn giản, responsive cơ bản)

-   Session/cookie để lưu favorite (chưa cần database)

-   Auth: ASP.NET Identity

> **🗂️ DB Table tối thiểu:**

-   Users (role: customer, host, admin)

-   Buildings, Rooms, RoomImages

-   ViewAppointments

-   BookingRequests

-   Amenities + RoomAmenities (nếu còn thời gian)

> **🧊 Các chức năng nên để lại cho giai đoạn sau:**

-   Chat

-   Hợp đồng thuê

-   Thông báo, thống kê

-   Tư vấn viên

-   Quản lý báo cáo vi phạm
