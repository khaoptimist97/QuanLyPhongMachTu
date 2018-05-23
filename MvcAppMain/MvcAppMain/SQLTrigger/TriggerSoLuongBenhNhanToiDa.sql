USE QLPhongMachTu

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER TG_SoLuongBenhNhanToiDaTrongNgay
ON dbo.PhieuKhamBenh
FOR INSERT,UPDATE
AS
BEGIN
	DECLARE @ngayKham DATE
	DECLARE @soLuongBenhNhan INT
	DECLARE @iD_BenhNhan INT

	SELECT @ngayKham = Inserted.NgayKham, @iD_BenhNhan = Inserted.ID_BenhNhan FROM Inserted

	SELECT @soLuongBenhNhan = COUNT(ID_BenhNhan) FROM dbo.HoSoBenhNhan WHERE ID_BenhNhan = @iD_BenhNhan

	IF(@soLuongBenhNhan > 40)
		BEGIN
			PRINT 'So benh nhan toi da la 40 nguoi moi ngay'
			ROLLBACK TRANSACTION;
		END
        
END
