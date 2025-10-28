-- Test insert (không nêu AgentId)
INSERT INTO RubberAgent (AgentCode, AgentName, OwnerName, TaxCode, AgentAddress, IsActive, CreatedAt, CreatedBy)
VALUES ('2001', N'Đại lý A', N'Nguyễn Văn A', '012327456', N'Q1', 1, GETDATE(), N'seedA')
      ,('2002', N'Đại lý B', N'Nguyễn Văn B', '098837665', N'Q1', 1, GETDATE(), N'seedB')
      ,('2003', N'Đại lý C', N'Nguyễn Văn C', '098756652', N'Q1', 1, GETDATE(), N'seedC')
      ,('2004', N'Đại lý D', N'Nguyễn Văn D', '096876651', N'Q1', 1, GETDATE(), N'seedD')
      ,('2005', N'Đại lý E', N'Nguyễn Văn E', '098476652', N'Q1', 1, GETDATE(), N'seedE')
INSERT INTO [RubberFarm] (
  [FarmCode],[AgentCode],[FarmerName],[FarmerPhone],[FarmerAddress],[FarmerMap],[Certificates],
  [TotalAreaHa],[RubberAreaHa],[TotalExploit],
  [IsActive],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy]
)
VALUES
('20001','2001',N'Vườn A1','0901000001',N'Q1, TP.HCM','10.77,106.68',N'VietGAP', 5.50,4.20,1200,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20002','2001',N'Vườn A2','0901000002',N'Q1, TP.HCM','10.78,106.69',N'FSC',    4.30,3.10, 950,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20003','2002',N'Vườn B1','0902000003',N'Thủ Đức, TP.HCM','10.84,106.76',N'VietGAP', 6.20,5.00,1500,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20004','2002',N'Vườn B2','0902000004',N'Thủ Đức, TP.HCM','10.85,106.75',N'',       3.80,2.60, 800,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20005','2003',N'Vườn C1','0903000005',N'Biên Hòa, Đồng Nai','10.95,106.82',N'FSC',    7.10,5.60,1700,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20006','2003',N'Vườn C2','0903000006',N'Biên Hòa, Đồng Nai','10.96,106.83',N'',       4.90,3.70,1100,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20007','2004',N'Vườn D1','0904000007',N'Tân Uyên, Bình Dương','11.06,106.76',N'VietGAP', 5.20,4.00,1150,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20008','2004',N'Vườn D2','0904000008',N'Tân Uyên, Bình Dương','11.05,106.75',N'',       3.60,2.40, 700,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20009','2005',N'Vườn E1','0905000009',N'Long Khánh, Đồng Nai','10.93,107.24',N'FSC',    6.80,5.10,1600,1,SYSUTCDATETIME(),N'seed',NULL,NULL),
('20010','2005',N'Vườn E2','0905000010',N'Long Khánh, Đồng Nai','10.94,107.23',N'',       4.10,3.00, 900,1,SYSUTCDATETIME(),N'seed',NULL,NULL);
