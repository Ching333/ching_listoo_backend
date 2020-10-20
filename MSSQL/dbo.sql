/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : SQL Server
 Source Server Version : 14002027
 Source Host           : DESKTOP-H3ECMEV:1433
 Source Catalog        : FEUNML
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 14002027
 File Encoding         : 65001

 Date: 14/10/2020 17:19:31
*/


-- ----------------------------
-- Table structure for place
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[place]') AND type IN ('U'))
	DROP TABLE [dbo].[place]
GO

CREATE TABLE [dbo].[place] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] nvarchar(100) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [longitude] decimal(11,8)  NOT NULL,
  [latitude] decimal(11,8)  NOT NULL,
  [phone] nvarchar(50) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [address] nvarchar(50) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [type] nvarchar(max) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [gmap_id] nvarchar(max) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[place] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'序號',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'名稱',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'經度',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'longitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'緯度',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'latitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'電話',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'phone'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地址',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'類型',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'googleAPI ID',
'SCHEMA', N'dbo',
'TABLE', N'place',
'COLUMN', N'gmap_id'
GO


-- ----------------------------
-- Records of place
-- ----------------------------
SET IDENTITY_INSERT [dbo].[place] ON
GO

SET IDENTITY_INSERT [dbo].[place] OFF
GO


-- ----------------------------
-- Table structure for placeList
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[placeList]') AND type IN ('U'))
	DROP TABLE [dbo].[placeList]
GO

CREATE TABLE [dbo].[placeList] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [user_id] int  NOT NULL,
  [name] varchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [description] varchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [privacy] varchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [created] datetime  NOT NULL,
  [updated] datetime  NULL,
  [cover] varbinary(1)  NULL
)
GO

ALTER TABLE [dbo].[placeList] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'序號',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'使用者ID',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'user_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'名稱',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文章',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'權限 0=公開 1=私有',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'privacy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'創建時間',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'created'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新時間',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'updated'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地點圖片',
'SCHEMA', N'dbo',
'TABLE', N'placeList',
'COLUMN', N'cover'
GO


-- ----------------------------
-- Records of placeList
-- ----------------------------
SET IDENTITY_INSERT [dbo].[placeList] ON
GO

SET IDENTITY_INSERT [dbo].[placeList] OFF
GO


-- ----------------------------
-- Table structure for placeRelation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[placeRelation]') AND type IN ('U'))
	DROP TABLE [dbo].[placeRelation]
GO

CREATE TABLE [dbo].[placeRelation] (
  [place_id] int  NOT NULL,
  [placeList_id] int  NOT NULL
)
GO

ALTER TABLE [dbo].[placeRelation] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'地方編號',
'SCHEMA', N'dbo',
'TABLE', N'placeRelation',
'COLUMN', N'place_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'清單編號',
'SCHEMA', N'dbo',
'TABLE', N'placeRelation',
'COLUMN', N'placeList_id'
GO


-- ----------------------------
-- Records of placeRelation
-- ----------------------------

-- ----------------------------
-- Table structure for tag
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tag]') AND type IN ('U'))
	DROP TABLE [dbo].[tag]
GO

CREATE TABLE [dbo].[tag] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] nvarchar(30) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [type] int  NOT NULL
)
GO

ALTER TABLE [dbo].[tag] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tag
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tag] ON
GO

SET IDENTITY_INSERT [dbo].[tag] OFF
GO


-- ----------------------------
-- Table structure for tagEvent
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tagEvent]') AND type IN ('U'))
	DROP TABLE [dbo].[tagEvent]
GO

CREATE TABLE [dbo].[tagEvent] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [user_id] int  NOT NULL,
  [tag_id] int  NULL,
  [event] int  NULL,
  [created] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[tagEvent] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'紀錄使用者事件。 0: 點擊篩選 1:取消篩選   2: 加入標籤 3:  取消標籤',
'SCHEMA', N'dbo',
'TABLE', N'tagEvent',
'COLUMN', N'event'
GO

EXEC sp_addextendedproperty
'MS_Description', N'觸發時間',
'SCHEMA', N'dbo',
'TABLE', N'tagEvent',
'COLUMN', N'created'
GO


-- ----------------------------
-- Records of tagEvent
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tagEvent] ON
GO

SET IDENTITY_INSERT [dbo].[tagEvent] OFF
GO


-- ----------------------------
-- Table structure for tagRelation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tagRelation]') AND type IN ('U'))
	DROP TABLE [dbo].[tagRelation]
GO

CREATE TABLE [dbo].[tagRelation] (
  [place_id] int  NOT NULL,
  [tag_id] int  NOT NULL,
  [user_id] int  NOT NULL,
  [created] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[tagRelation] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tagRelation
-- ----------------------------

-- ----------------------------
-- Table structure for user
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type IN ('U'))
	DROP TABLE [dbo].[user]
GO

CREATE TABLE [dbo].[user] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] nvarchar(50) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [email] nvarchar(50) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [password] nvarchar(50) COLLATE Chinese_Taiwan_Stroke_CI_AS  NOT NULL,
  [authority] int  NOT NULL,
  [updated] datetime2(0)  NULL,
  [created] datetime2(0)  NULL
)
GO

ALTER TABLE [dbo].[user] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'序號',
'SCHEMA', N'dbo',
'TABLE', N'user',
'COLUMN', N'id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'user',
'COLUMN', N'name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'電郵(帳號)',
'SCHEMA', N'dbo',
'TABLE', N'user',
'COLUMN', N'email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密碼',
'SCHEMA', N'dbo',
'TABLE', N'user',
'COLUMN', N'password'
GO

EXEC sp_addextendedproperty
'MS_Description', N'0=管理者 1=一般 2=刪除',
'SCHEMA', N'dbo',
'TABLE', N'user',
'COLUMN', N'authority'
GO


-- ----------------------------
-- Records of user
-- ----------------------------
SET IDENTITY_INSERT [dbo].[user] ON
GO

INSERT INTO [dbo].[user] ([id], [name], [email], [password], [authority], [updated], [created]) VALUES (N'2', N'test', N'test', N'test', N'123', N'2020-10-06 12:31:03', N'2020-10-06 12:31:05')
GO

SET IDENTITY_INSERT [dbo].[user] OFF
GO


-- ----------------------------
-- Table structure for 拉麵清單50-工作表1
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[拉麵清單50-工作表1]') AND type IN ('U'))
	DROP TABLE [dbo].[拉麵清單50-工作表1]
GO

CREATE TABLE [dbo].[拉麵清單50-工作表1] (
  [f1] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [店名] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [地址] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [營業時間] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [Info] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤1] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤2] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤3] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤4] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤5] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤6] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤7] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤8] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤9] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL,
  [標籤10] nvarchar(255) COLLATE Chinese_Taiwan_Stroke_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[拉麵清單50-工作表1] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of 拉麵清單50-工作表1
-- ----------------------------
INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'1', N'辣麻味噌沾麵 鬼金棒(總店沾麵部)', N'台北市中山區中山北路一段94號', N'週一-五 12:00–14:00、17:00–20:30 週六-日 12:00–20:30', N'來自日本東京，口味在日本台灣都是自成一派的「辣麻味噌拉麵」。總店為沾麵專賣店，販賣品項為可調整辣度麻度的辣麻味噌沾麵以及博多豚骨拉麵。店主對於食材挑選極為用心，店內叉燒除了類似控肉的角煮外，也有低溫烹調的和牛及依比利豬叉燒可供選擇，白飯使用伊馬嵐池上米，博多豚骨拉麵使用杜蘭小麥。店內不時會有特殊限定，建議前往前先參考官方粉專。', N'拉麵', N'沾麵', N'辣味', N'麻味', N'味噌', N'豚骨', N'豚骨魚介', N'2019金賞', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'2', N'辣麻味噌拉麵 鬼金棒(總店拉麵部)', N'台北市中山區中山北路一段94號', N'週一-五 12:00–14:00、17:00–20:30 週六-日 12:00–20:31', N'來自日本東京，口味在日本台灣都是自成一派的「辣麻味噌拉麵」。本店為拉(湯)麵專賣店，販賣品項為可調整辣度麻度的辣麻味噌拉麵以及鬼金棒的豚骨拉麵。店內招牌的辣麻味噌拉麵是混合豚骨及魚介兩種湯底（目前夏日為豚雞魚介湯）再混入味噌，麵條也是三種不同麵條混合，叉燒則是類似控肉的角煮，在在顯露出強烈的自我風格。店內不時會有特殊限定，建議前往前先參考官方粉專', N'拉麵', N'辣味', N'麻味', N'味噌', N'豚骨', N'豚骨魚介', N'2019金賞', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'3', N'辣麻味噌拉麵 鬼金棒(松江南京店)', N'台北市中山區松江路123巷8-2號', N'11:30–14:00、17:00–20:30', N'來自日本東京，口味在日本台灣都是自成一派的「辣麻味噌拉麵」。南京松江店為拉(湯)麵專賣店，販賣品項為可調整辣度麻度的辣麻味噌拉麵以及鬼金棒的豚骨拉麵。與中山店不同的是店內的辣麻味噌拉麵是只使用豚骨單一湯底再混入味噌，麵條也是單一的粗麵，叉燒則是類似控肉的角煮，是最接近日本本店的味道。麻辣的程度也是三店之冠，角煮也是最為穩定且大塊的一家分店。', N'拉麵', N'辣味', N'麻味', N'味噌', N'豚骨', N'2019金賞', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'4', N'雞吉君拉麵とりよしくんラーメン雞白湯專門店', N'台北市內湖區環山路一段33號', N'週一休 適逢每月雙數週休一、二 週二~四 17:30~21:00 週五~日 11:30~14:00、17:30~21:00', N'雞吉君是雞湯愛好者必朝聖的一間店，口味上從清淡到濃厚分別是淡麗->濃厚->特濃，三種口味各有千秋，可以按照自己的喜好做選擇。叉燒飯則有分大小，唯一須注意是店內無廁所，請留心。(週日中午有提供煮干拉麵)', N'拉麵', N'油拌麵', N'淡麗', N'雞白', N'雞濃', N'雞清', N'煮干', N'2019金賞', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'5', N'勝王', N'台北市中山區林森北路306號', N'11:30~14:00、17:00~20:30', N'2020年8月22日重新開幕，本店為台灣第一個二毛作(原意為同一塊耕地一年內種植不同作物，在餐飲業為在不同時間提供不同服務及商品)的拉麵店。勝王在台灣是間實驗性質相當高的店家，除了經常出現各種特別煮干、魚類食材外，各式醬油、香料、麵體、甚至水也是店家嘗試的對象，可以說是台灣新潮流拉麵的頂點，每週的特別食材除了吸引大量拉麵愛好者前來之外，台灣的店家，甚至日本拉麵店長也都前來嘗試過。', N'拉麵', N'雞白', N'限定', N'變化', N'2019金賞', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'6', N'五之神製作所 台灣', N'台北市信義區忠孝東路四段553巷6弄6號', N'週一~五 11:30~15:00、17:00~21:00 週二 公休 週六~日 11:30~21:00', N'為拉麵界較為少見的，以蝦作為基底的拉麵店，一共分為豚骨蝦湯麵、蝦沾麵與番茄蝦沾麵，其叉燒飯則為相當傳統的鹹甜醬香口味，其周末經常有限定沾麵/拉麵與對應的限定飯，均為水準之作，惟其麵條鹼水味較為鮮明，還請留意。', N'拉麵', N'沾麵', N'豚骨', N'豚骨魚介', N'蝦豚骨', N'限定', N'變化', N'2019金賞', N'雞白湯', N'冷凍包')
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'7', N'Okaeriお帰り吃碗拉麵吧', N'台北市大安區延吉街60號', N'週二~週日: 11:30～15:00或（售完為止）17:30～21:00或（售完為止） 每週一公休（每月第二及第四週的週一和週二也為公休日）', N'開拓台灣純正日本口味拉麵疆土的老店之一、與雞吉君為同一店主。店內基本為熬煮12小時以上的濃厚豚骨湯頭為主，厚實順口的豚骨湯加上厚切夠份量的叉燒、清甜爽口的高麗菜，再以近乎偏執的完美擺盤，在台灣是辨識度相當高的一家店。除了豚骨湯底之外，也有京都醬油拉麵、大阪炒麵、甚至咖哩飯等品項，店內現點現包的煎餃其美味及數量稀少，往往在一開店即銷售一空。', N'拉麵', N'豚骨', N'豚骨魚介', N'辣味', N'煎餃', N'2019金賞', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'8', N'隱家拉麵-士林店', N'台北市士林區中山北路五段505巷1號', N'11:30~14:30、16:30~22:00', N'開業於2019年10月，本店的風格為多樣化的湯頭，目前提供雞湯與豚骨湯二種湯頭，以及濃厚蝦沾麵的品項。店主對於拉麵的專精與與人為善的熱情，造就本店的自信美味，但店主想在此店傳達除了美味之外，更在乎店家與食客之間心的連繫，因此創立了隱家拉麵，希望可以與食客一起分享美食與生活。', N'拉麵', N'沾麵', N'豚骨', N'雞清', N'豚骨蝦', N'限定', N'加麵一球免費', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'9', N'隱家拉麵-赤峰店', N'台北市大同區南京西路25巷28號', N'11:30~14:00、16:30~22:00', N'開幕於2020年5月，本店為隱家拉麵二店，主打黃金雞湯、辛豚骨，並有每日限定擔擔麵、鯛白湯沾麵享用。', N'拉麵', N'雞清', N'豚骨', N'魚白', N'擔擔麵', N'加麵一球免費', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'10', N'柑橘 Shinn', N'台北市大安區仁愛路四段228-6號', N'週一~五 11:30 - 14:00、17:00 - 20:30 週六~日 11:30 - 20:30', N'開業於2017年3月，融合美日台三種文化風格融合的油拌麵，面對的族群以女性客人及上班族居多，自推出柑橘系列的拉麵之後，大受各方饕客好評，目前首推 柑橘蛤蜊鹽味淡麗雞清湯 ，是台灣難得一見的淡麗味覺，後續推出鴨蔥拉麵，饕客間皆有不錯的好評，詳情請見下方食記。', N'拉麵', N'油拌麵', N'柑橘', N'雞白', N'雞濃', N'雞清', N'淡麗', N'2019金賞', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'11', N'山嵐拉麵-台灣總店', N'台北市大安區杭州南路二段75號', N'11:30–14:30、17:15–20:45', N'於2019年6月遷移至現址，店內所售品項分為白湯(豚骨)、赤湯(加辣)、海湯(加魚粉)，三種口味也各有沾麵與蔥爆、叉燒版本，均為特色與水準之作，柔嫩的叉燒與風味十足的風味油更是在吃時讓人相當喜愛，沾湯內的高麗菜滋味十足，湯頭內的豬背脂油而不膩讓人回味再三。', N'拉麵', N'沾麵', N'豚骨', N'豚骨魚介', N'辣味', N'2019金賞', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'12', N'山嵐拉麵-公館店', N'台北市中正區羅斯福路四段136巷1弄13號', N'週日~周四: 11:30~21:00 周五~周六: 11:30~21:30', N'在東區的分店開設後，山嵐決定於公館開設第二間店，本店除了擁有與忠孝店相同的定番外，更常常推出與忠孝店不同的限定以饗饕客；而鄰近的公館商圈除了有為數眾多的夜市小吃外，更有金石文化廣場與東南亞秀泰影城可供遊憩。', N'拉麵', N'沾麵', N'豚骨', N'豚骨魚介', N'辣味', N'2019金賞', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'13', N'山嵐拉麵-大安店', N'台北市大安區復興南路二段', N'11:30~14:30、17:15~20:45', N'於2019年11月開幕，店內所售品項分為白湯(豚骨)、赤湯(加辣)、海湯(加魚粉)，三種口味也各有沾麵與蔥爆、叉燒版本，均為特色與水準之作，柔嫩的叉燒與風味十足的風味油更是在吃時讓人相當喜愛，沾湯內的高麗菜滋味十足，湯頭內的豬背脂油而不膩讓人回味再三。本店目前的期間限定為濃厚番茄拉麵、海鮮咖哩沾麵。', N'拉麵', N'沾麵', N'豚骨', N'豚骨魚介', N'蕃茄', N'咖哩', N'2019金賞', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'14', N'拉麵公子', N'台北市中山區八德路二段279號', N'11:30~13:30、17:30~20:30', N'開業於2019年12月3日，此店為知名拉麵部落客，品嘗各地拉麵吃出心得，轉而開店的範例之一，本店是以雞湯與進階型煮干為主軸的店家，雞湯甘美香醇，煮干苦鮮醇厚。', N'拉麵', N'雞白', N'雞清', N'煮干', N'變化', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'15', N'麵屋壹慶', N'台北市中山區雙城街32巷2號1 1c號', N'17:00–21:00 星期一、日 公休', N'開業於2018年9月，台灣首間以泡系為主題的店家，豚骨醬油、豚骨塩味、清湯醬油以上三種味道都有其特色，領先市場推出的泡系雞白湯，湯溫心暖醇郁順口，亦有不錯的好評。', N'拉麵', N'雞白', N'豚骨', N'泡系', N'醬油', N'2019金賞', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'16', N'麵屋壹の穴 ICHI', N'台北市大安區延吉街137巷22號', N'11:30~15:00，17:30~21:00 週一公休', N'老闆號稱180，除了招牌的魚介湯頭外，亦開發了豚骨、雞湯、煮干、醬油等湯頭，也有日式擔擔麵，是少數可以品嚐到多種拉麵的店家。除了這些之外，壹之穴也有推出大份量的二郎系拉麵，分成壹之二郎與痛風二郎兩種，份量相當大。除此之外，壹之穴除了汁無し擔擔麵以外都可以免費加細麵一次，份量與價格皆相當實在。除了多樣化的拉麵外，其他副食則有相當受到好評的叉燒飯、唐揚雞塊、醃漬小番茄可供選擇。配料的部分叉燒和溏心蛋皆可以加點，中午用餐時段也有優惠的套餐組合。', N'拉麵', N'油拌麵', N'豚骨', N'豚骨魚介', N'二郎風', N'限定', N'變化', N'2019金賞', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'17', N'創作拉麵 悠然', N'台北市中山區林森北路487號1 樓 209 室', N'週三~六 17:00~20:00 週日 11:00~14:00，17:00~20:00 週一~二 公休', N'店長繼承過往拉麵店家修業經驗，加上自身對於拉麵的詮釋及對品質的堅持，重新定義了"台灣拉麵"一詞。從初期第一碗名為"琢磨"的虱目魚鹽味拉麵，創作了以季節為主的十二碗拉麵，並不時創作以節慶為主的限定拉麵，以嚮麵友們的味蕾。目前是以一週固定只販賣一種拉麵的方式經營，每週販賣品項請參考粉專。', N'拉麵', N'淡麗系', N'限定', N'雞清湯', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'18', N'麵屋武藏本店', N'台北市中正區忠孝西路一段36號B1', N'11:00~22:00', N'來自日本的品牌，開啟W湯頭的領航者，位於HOYII和億B2，不同於麵屋武藏-神山，本店以雞白湯頭為主打，有沾麵和拉麵可供選擇。', N'拉麵', N'沾麵', N'雞白', N'加麵一球免費', N'限定', N'變化', N'2019名店', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'19', N'道樂屋台', N'台北市士林區基河路112號', N'17:30~24:00 週一 公休', N'"屋台"(やたい,YATAI) 就是所謂的路邊攤，晚上才開始營業，與一般日式居酒屋不同，反而像是台灣的路邊攤，形成福岡當地的特色。道樂屋台是全台最為熱門的屋台之一，不定時出現的限定餐點，也受到麵友們的喜愛。', N'拉麵', N'豚骨', N'雞湯', N'加麵一球免費', N'限定', N'變化', N'2019名店', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'20', N'道樂ラーメン專門店', N'台北市士林區大北路9號', N'17:00~23:30 週一 公休', N'開業於2018年8月，位於士林夜市文林路旁，與原道樂屋台味道迥異的拉麵店，主打平民小吃，屋台風情，店內的豪華豚骨、蛤蜊豚骨、海老豚骨拉麵都有其愛好者，不定時出現的限定餐點，也受到麵友們的喜愛。', N'拉麵', N'豚骨', N'雞白', N'家系', N'限定', N'變化', N'加麵一球免費', N'2019名店', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'21', N'道樂商店', N'台北市萬華區成都路27巷12號', N'12:00~14:00、17:00~21:00 週一 公休', N'於2020年5月開幕，本店為道樂四店，主打平民小吃，屋台風情，店內的豪華豚骨、蛤蜊豚骨、海老豚骨拉麵都有其愛好者，不定時出現的限定餐點，也受到麵友們的喜愛。', N'拉麵', N'豚骨', N'雞白', N'家系', N'限定', N'變化', N'加麵一球免費', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'22', N'吉天元拉麵店', N'台北市萬華區西寧南路70號B1', N'週二-五 12:00~15:00、17:00~21:00 週六-日 12:00~21:00 星期一 公休', N'開業於2018年10月，位於西門町萬年大樓B1，湯頭是以雞食材為主軸，湯頭濃醇鮮鹹甘，是西門町區域的優選雞白湯拉麵店，主推野菜雞白湯拉麵，不定時會推出限定以嚮麵友們的味蕾。', N'拉麵', N'雞白', N'泡系', N'限定', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'23', N'鷹流拉麵【台湾本店】', N'台北市大安區忠孝東路四段216巷27弄14號', N'週一~六 11:30–22:00 週日 11:30–21:00', N'來自東京高田馬場，號稱全台最濃厚的鹽味豚骨拉麵，是台灣濃厚豚骨湯頭的先驅，搭配上全粒粉的多加水自家製麵，拉麵風格跟店主鷹峰一樣相當強烈。每個月29日會有叉燒增量的活動，限定拉麵多為每季一次，另外每年年初發售的月曆也是拉麵界盛事。老闆目前在台北六家口味不同的分店，為非連鎖體系中擁有分店最多的。', N'拉麵', N'沾麵', N'豚骨', N'豚濃', N'限定', N'每月29號', N'2019名店', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'24', N'真劍拉麵', N'台北市大安區師大路126巷1號', N'11:30~14:00，17:30~21:30', N'開幕於2018年11月，以鹽味翠雞拉麵擁有的藍色雞湯味湯頭而知名的拉麵店，是台灣IG照片上的常客，店門口吊個沙包也是特色，是屬於師大區域的知名店家，詳情請見下方食記。', N'拉麵', N'雞清', N'豚骨', N'加麵一球免費', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'25', N'雞二拉麵-本店', N'台北市大安區文昌街30號', N'11:30~20:30 週日 公休', N'主打超大份量，且價格實惠的雞湯拉麵，主要分成三種分量：小小雞、小雞二、大雞二。大雞二是許多大食量麵友喜愛挑戰的目標，小雞二則適合一般食量的麵友。經典款的拉麵有醬油和鹽味，也有推出如蝦醬雞白湯等特殊口味，若愛吃鹹的麵友，建議味道加鹹，會感覺更加豐厚。', N'拉麵', N'雞白', N'雞魚介', N'二郎風', N'大食', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'26', N'雞二二店', N'台北市大安區羅斯福路三段139號', N'11:30~20:30 週日 公休', N'主打超大份量，且價格實惠的雞湯拉麵，主要分成三種分量：小小雞、小雞二、大雞二。大雞二是許多大食量麵友喜愛挑戰的目標，小雞二則適合一般食量的麵友。經典款的拉麵有醬油和鹽味，也有推出如蝦醬雞白湯等特殊口味，若愛吃鹹的麵友，建議味道加鹹，會感覺更加豐厚。', N'拉麵', N'雞白', N'雞魚介', N'二郎風', N'大食', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'27', N'豚人ラ－メン_敦化本店', N'台北市大安區忠孝東路四段170巷5弄10號', N'週日~四 11:30~21:00(L.O 20:30) 週五~六 11:30~27:00(L.O 26:30)', N'於2018年7月2日試營運，10月3日開幕，本店為豚人台灣本店，此店承襲前夢語店面，是來自京都的拉麵店，主要顧客以學生居多，特色是軟嫩叉燒肉與多樣性的麵條，目前提供三種麵條，以及二郎旁系拉麵，深受大食麵友喜愛，詳情可察看下方網友食記。', N'拉麵', N'豚骨', N'二郎風', N'大食', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'28', N'豚人ラ－メン-復興店', N'台北市大安區大安路一段16巷6號', N'週一 12:00~15:00、17:00~22:00 週二 公休 週三 17:00~22:00 週四 12:00~15:00、17:00~22:00 週五~六 12:00~15:00、17:00~26:00 週日 12:00~15:00、17:00~22:00', N'開業於2020年2月14日，來自京都的拉麵店，特色是軟嫩叉燒肉與多樣性的麵條，目前提供三種麵條，以及二郎旁系拉麵，深受大食麵友喜愛，本店的限定味道為辣味鹽豚骨，亦有不少客人喜愛，詳情可察看下方網友食記。', N'拉麵', N'豚骨', N'二郎風', N'限定', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'29', N'鳥人拉麵-台灣總店TOTTO Ramen', N'台北市大安區復興南路一段107巷5弄9號', N'週日~四 : 11:30~22:00 週五~六 : 11:30~28:00', N'來自紐約的雞湯名店，此間為台灣總本店，品項有雞湯、雞湯味噌，與一般少見的，以蔬菜打底的素食拉麵，低溫雞叉燒與一般的豬肉叉燒都相當可口，辣味噌與味噌品項更是適合重口味人士；此外特定營業日其營業時段直至凌晨4點，相當適合晚歸的朋友在凌晨來一碗熱呼呼的拉麵提振心情。', N'拉麵', N'雞白', N'咖哩', N'每月17日', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'30', N'Totto Ramen 鳥人拉麵-西門店', N'台北市萬華區中華路一段114巷3號', N'週日~一 : 11:30–22:00 週五~六 : 11:30–28:00', N'來自紐約的雞湯名店，此間為第三家分店，品項有雞湯、雞湯味噌，與一般少見的，以蔬菜打底的素食拉麵，低溫雞叉燒與一般的豬肉叉燒都相當可口，辣味噌與味噌品項更是適合重口味人士；此外特定營業日其營業時段直至凌晨4點，相當適合晚歸的朋友在凌晨來一碗熱呼呼的拉麵提振心情。', N'拉麵', N'雞白', N'咖哩', N'2019名店', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'31', N'Totto Ramen 鳥人拉麵-中山店', N'台北市中山區中山北路一段105巷7號', N'週日~一 : 11:30~22:00 週二~六 : 11:30~28:00', N'來自紐約的雞湯名店，此間為二店，服務的客人大多是以七條通的客人為主，品項有雞湯、雞湯味噌，與一般少見的，以蔬菜打底的素食拉麵，低溫雞叉燒與一般的豬肉叉燒都相當可口，辣味噌與味噌品項更是適合種口味人士；此外特定營業日其營業時段直至凌晨4點，相當適合晚歸的朋友在凌晨來一碗熱呼呼的拉麵提振心情。', N'拉麵', N'雞白', N'咖哩', N'素食', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'32', N'麵屋昕家', N'台北市內湖區東湖路24號', N'週二~四 17:00~20:30 週五~日 11:30~13:30、17:00~20:30 週一 公休', N'開業於2019年9月，修業自麵屋真登，湯頭屬於雞湯系味道舒服和緩的路線。', N'拉麵', N'雞白', N'雞清', N'泡系', N'醬油', N'限定', N'變化', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'33', N'橫濱家系ラーメン大和家', N'台北市大安區市民大道四段12號', N'11:30–22:30', N'日本發祥台灣初上陸「橫濱家系大和家拉麵」是直營的拉麵連鎖店，在日本目前擁有11家分店，且持續擴大中。 “最強家系拉麵” 在競爭激烈的日本拉麵市場中，期望以濃厚豚骨醬油湯頭、Q彈中粗直條麵，自豪的橫濱家系大和家拉麵能成為拉麵業界NO.1，因此將滿腔熱血化為文字，朝「家系最強」目標邁進。', N'拉麵', N'家系', N'豚骨', N'每月1號', N'每月20號', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'34', N'麵屋一燈 Menya Itto Taiwan', N'台北市中山區南京東路一段29號', N'11:30 - 21:30', N'來自日本，長年雄霸Ramen DB的名店，其招牌濃厚魚介沾麵沾湯為相當特別的雞骨魚介風味，低溫雞肉叉燒與豬肉叉燒軍軟嫩彈牙而無纖維感，非常可口。其拉麵款式中，最為出眾的則為干貝雞湯拉麵，在一般雞湯湯底的香甜之餘，更可額外加入干貝醬增添醇厚風味。此外，也有當日限定的副食可以加點，均相當有特色。', N'拉麵', N'沾麵', N'雞煮干', N'雞魚介', N'點餐前加麵免費', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'35', N'麵屋一燈Express', N'台北市中山區南京西路14號', N'11:00~22:00', N'開業於2019年11月，位於B2誠品信義美食街。', N'拉麵', N'沾麵', N'雞魚介', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'36', N'橫濱家系拉麵 特濃屋', N'台北市中山區中山北路二段77巷22號', N'11:30-15:00、17:00-22:00 週一公休', N'主打橫濱家系的拉麵，豪邁大海苔和菠菜是特色之一。炸雞是麵友推薦必點項目，所以又有炸雞屋的別稱。', N'拉麵', N'豚骨', N'家系', N'辣味', N'炸雞', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'37', N'墨洋拉麵', N'台北市中正區羅斯福路四段136巷10號', N'12:00 - 14:30 、17:00~21:00', N'於2020年1月18日於公館商圈開業，是屬於台灣少見魚貝系店家，主打貝系醬油風味，限定品項皆為海鮮品系。', N'拉麵', N'魚貝', N'限定', N'變化', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'38', N'麵屋武藏-神山', N'台北市中山區中山北路一段121巷18號', N'11:30 - 21:30 深夜食堂：週一～週六 21:30 - 23:30', N'來自日本的品牌，在日本是"動物系"加上"魚介系"雙湯頭的創始名店。神山店延續日本雙湯頭的特色，加上粗麵以及豪邁的大塊炙燒叉燒，為台灣拉麵風潮的開拓者之一，也是屬於深夜食堂之中的名店。', N'拉麵', N'沾麵', N'豚骨魚介', N'布丁', N'限定', N'變化', N'加麵一球免費', N'2019名店', NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'39', N'鷹流東京醤油拉麺【蘭丸ranmaru】延吉街店', N'台北市松山區延吉街2號', N'11:30–14:00，17:30–21:30', N'來自鷹流老闆鷹峰涼一，魚介風味的醬油拉麵，湯底有清湯以及濃湯可供選擇，店內使用的是自家製麵的細麵，可以無限加麵，味付玉子(糖心蛋)以及生玉子(生蛋)都是自由捐獻，一個30元起。蘭丸雖然是醬油拉麵，但是口味跟鷹流一樣相當強烈而且豪邁，搭配齒切感十足的細麵以及鮮嫩的叉燒，排隊人潮絡繹不絕，該店也是台灣第一個使用弓削多醬油，以及將叉燒排成肉圈圈的店家，每月29日有特別叉燒增量活動。有別於另一家中山店，本店限定為弓削多生薑醬油拉麵。', N'拉麵', N'醬油', N'豚骨魚介', N'加麵一球免費', N'每月29號', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'40', N'鷹流東京醤油拉麺【蘭丸ranmaru】中山店', N'台北市大同區南京西路18巷6-3號', N'11:30–14:00，17:00–22:00', N'來自鷹流老闆鷹峰涼一，魚介風味的醬油拉麵，湯底有清湯以及濃湯可供選擇，店內使用的是自家製麵的細麵，可以無限加麵，味付玉子(糖心蛋)以及生玉子(生蛋)都是自由捐獻，一個30元起。蘭丸雖然是醬油拉麵，但是口味跟鷹流一樣相當強烈而且豪邁，搭配齒切感十足的細麵以及鮮嫩的叉燒，排隊人潮絡繹不絕，該店也是台灣第一個使用弓削多醬油，以及將叉燒排成肉圈圈的店家，每月29日有特別叉燒增量活動。有別於另一家延吉店，本店限定為鹽味拉麵。', N'拉麵', N'醬油', N'豚骨魚介', N'加麵一球免費', N'每月29號', N'2019名店', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'41', N'鷹流東京豚骨拉麺【極匠gokujoh】公館店', N'台北市中正區汀州路三段104巷4號', N'11:30–14:30、17:00–21:00', N'來自鷹流老闆鷹峰涼一，加入背脂的豚骨濃湯，以及自家製麵的粗麵，可以加麵二球但是加麵只限細麵，味付玉子(糖心蛋)以及生玉子(生蛋)都是自由捐獻，一個30元起。除了濃湯外，還有搭配細麵的博多拉麵，博多拉麵沒有蒜末，但是會加上一匙辣醬，每月29日有特別叉燒增量活動。', N'拉麵', N'豚骨魚介', N'加麵一球免費', N'每月29號', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'42', N'鳳華拉麵 （らぁ麺 鳳華）', N'台北市內湖區內湖路一段411巷6號', N'週一~五 11:45~14:45、17:30~21:00 週六~日 12:00~15:00、17:30~21:00', N'開業於2019年9月，店主修業自鷹流，以雞湯系為主體，湯體的鮮味層次突出，並主打泡系雞白湯拉麵，不定時出現的限定拉麵，讓內湖的麵友們有耳目一新的感覺，詳情可察看下方網友食記。', N'拉麵', N'雞白', N'泡系', N'雞清', N'限定', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'43', N'一番星拉麵', N'台北市內湖區內湖路一段285號101攤位2樓', N'週一~五 11:30~14:00、17:30~19:30 週六、日 公休', N'位於西湖市場2樓飲食區內，除了有豚骨品項以外，也不定期會推出限定品項，同時由於位於飲食區，座位動線也與一般美食街相若，較為寬敞。', N'拉麵', N'雞湯', N'限定', N'2019名店', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'44', N'一幻拉麵台北信義店', N'台北市信義區松壽路30號1樓', N'週一到週五：11:00～15:00，17:00～22:30 週六日：11:30～15:30，17:00～23:00', N'來自北海道的一幻拉麵，主打鮮蝦湯頭，混合豚骨湯頭，佐以味噌、醬油、鹽味等調味，相當具有特色且適合喜愛蝦味的朋友。', N'拉麵', N'豚骨', N'2019名店', N'豚骨蝦', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'45', N'博多幸龍総本店', N'台北市松山區八德路二段366巷16號', N'週一~六 11:00~14:30(L.O14:30) 17:00~27:00(L.O 26:30) 週日 11:00~21:00', N'開業於2019年3月，以豚骨湯為主體，是博多一幸舍的子品牌，湯品稠度中等，沒有一幸舍般的豚骨重味，是適合全家人皆可享用的豚骨味覺，麵友們建議本店湯品不建議主動加鹹，將會失去味道的平衡感，。', N'拉麵', N'豚骨', N'煽り系', N'限定', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'46', N'麵屋壹の穴ichi-沾麵專門店', N'台北市大安區復興南路二段350號', N'12:00~14:00、17:00~20:00 週日 公休', N'預定開業於2019年11月，為壹之穴系列第四間店，一、三、五出咖哩沾麵，二、四、六出煮干沾麵。', N'沾麵', N'豚雞', N'咖哩', N'煮干', N'限定', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'47', N'麵屋 千雲 -Chikumo-', N'台北市中山區林森北路105-1號', N'12:00–15:00、18:00–03:00', N'開幕於2019年2月，前身為鏡花水月拉麵店，之後轉型為麵屋千雲雞白湯拉麵店，湯內有雞肉丸是其特色，並且每日營業至凌晨3點，是台北市少見的深夜拉麵店。', N'拉麵', N'沾麵', N'雞白', N'雞濃', N'2019名店', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'48', N'一蘭拉麵-台灣台北本店', N'台北市信義區松仁路97號', N'全年無休', N'源自福岡博多的豚骨拉麵，以其在全世界都能維持一貫品質和因地制宜調整而聞名，是全世界數一數二的連鎖拉麵品牌。', N'拉麵', N'豚骨', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'49', N'一蘭拉麵-台灣台北本店別館', N'台北市信義區松壽路11號', N'全年無休', N'源自福岡博多的豚骨拉麵，以其在全世界都能維持一貫品質和因地制宜調整而聞名，是全世界數一數二的連鎖拉麵品牌。', N'拉麵', N'豚骨', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[拉麵清單50-工作表1] ([f1], [店名], [地址], [營業時間], [Info], [標籤1], [標籤2], [標籤3], [標籤4], [標籤5], [標籤6], [標籤7], [標籤8], [標籤9], [標籤10]) VALUES (N'50', N'太陽蕃茄拉麵-站前本店', N'台北市中正區忠孝西路一段38號凱撤飯店B1F', N'週日~一 11:00–22:00 週五~六 11:00–22:30', N'太陽蕃茄拉麵源自於日本「大阪王將」企業體系中的拉麵業態，「大阪王將」在日本當地及海外已擁有300餐廳以上的EAT&株式會社。品牌誕生的概念是「能保存美味及兼顧身體健康的日式拉麵」，第一家店誕生於2006年在日本東京的錦系町，以東京23區為中心開出17家店，而海外於2012年10月在台灣台北凱撒飯店B1美食廣場開出第一家，目前陸續籌畫展店中。太陽蕃茄拉麵以創新拉麵概念''「美味、美容、健康」，其創新獨特趕在日本及台灣人氣上昇中。', N'拉麵', N'雞清', N'蕃茄', N'義式', N'素食', N'限定', N'變化', N'2019名店', NULL, NULL)
GO


-- ----------------------------
-- Auto increment value for place
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[place]', RESEED, 1)
GO


-- ----------------------------
-- Uniques structure for table place
-- ----------------------------
ALTER TABLE [dbo].[place] ADD CONSTRAINT [UQ__place__name] UNIQUE NONCLUSTERED ([name] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table place
-- ----------------------------
ALTER TABLE [dbo].[place] ADD CONSTRAINT [PK__place__3213E83FF0E1D439] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for placeList
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[placeList]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table placeList
-- ----------------------------
ALTER TABLE [dbo].[placeList] ADD CONSTRAINT [PK__placeLis__3213E83FB7C38A0F] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table placeRelation
-- ----------------------------
ALTER TABLE [dbo].[placeRelation] ADD CONSTRAINT [PK__placeRel__F7DCF25635F50E0E] PRIMARY KEY CLUSTERED ([place_id], [placeList_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tag
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tag]', RESEED, 1)
GO


-- ----------------------------
-- Uniques structure for table tag
-- ----------------------------
ALTER TABLE [dbo].[tag] ADD CONSTRAINT [UQ__tag__name] UNIQUE NONCLUSTERED ([name] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table tag
-- ----------------------------
ALTER TABLE [dbo].[tag] ADD CONSTRAINT [PK__tag__3213E83FEED76E4D] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tagEvent
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tagEvent]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table tagEvent
-- ----------------------------
ALTER TABLE [dbo].[tagEvent] ADD CONSTRAINT [PK_tagEvent] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table tagRelation
-- ----------------------------
ALTER TABLE [dbo].[tagRelation] ADD CONSTRAINT [PK__tagRelat__C5BBBC5677E7CF1F] PRIMARY KEY CLUSTERED ([place_id], [tag_id], [user_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for user
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[user]', RESEED, 2)
GO


-- ----------------------------
-- Uniques structure for table user
-- ----------------------------
ALTER TABLE [dbo].[user] ADD CONSTRAINT [Email] UNIQUE NONCLUSTERED ([email] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table user
-- ----------------------------
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK__users__3213E83FF1B91C88] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table placeList
-- ----------------------------
ALTER TABLE [dbo].[placeList] ADD CONSTRAINT [fk_placeList_user_1] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table placeRelation
-- ----------------------------
ALTER TABLE [dbo].[placeRelation] ADD CONSTRAINT [fk_placeRelation_place_1] FOREIGN KEY ([place_id]) REFERENCES [dbo].[place] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[placeRelation] ADD CONSTRAINT [fk_placeRelation_placeList_1] FOREIGN KEY ([placeList_id]) REFERENCES [dbo].[placeList] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table tagEvent
-- ----------------------------
ALTER TABLE [dbo].[tagEvent] ADD CONSTRAINT [fk_tagEvent_user_1] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[tagEvent] ADD CONSTRAINT [fk_tagEvent_tag_1] FOREIGN KEY ([tag_id]) REFERENCES [dbo].[tag] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table tagRelation
-- ----------------------------
ALTER TABLE [dbo].[tagRelation] ADD CONSTRAINT [fk_tagRelation_user_1] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[tagRelation] ADD CONSTRAINT [fk_tagRelation_place_1] FOREIGN KEY ([place_id]) REFERENCES [dbo].[place] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[tagRelation] ADD CONSTRAINT [fk_tagRelation_tag_1] FOREIGN KEY ([tag_id]) REFERENCES [dbo].[tag] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

