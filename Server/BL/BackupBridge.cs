using System;
using System.Data;
using System.Data.SqlClient;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using Server.DAL;
using Amazon.Runtime.CredentialManagement;
using Amazon;
using Amazon.S3.Model;

namespace Server.BL
{
    interface IBackupPlan
    {
        void DumpData();
    }

    class AbstractBackupManager
    {
        IBackupPlan plan;

        public AbstractBackupManager(IBackupPlan plan)
        {
            this.plan = plan;
        }

        public void DumpData()
        {
            this.plan.DumpData();
        }
    }

    class OfflineBackup : IBackupPlan
    {
        OrderContext context = new OrderContext();
        // @TODO dump to storage
        public void DumpData()
        {
            string backupName = "MajesticDB" + DateTime.Now.ToString("yyyyMMddHHmm");
            const string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK ='E:\BackupDB.Bak' WITH NOFORMAT, NOINIT,  NAME = N'MajesticDb-Ali-Full Database Backup',
                        SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            int path = context.Database.ExecuteSqlCommand(
                System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction,
                string.Format(sqlCommand, "ServerDataMark50", backupName)
                );
        }
    }

    class OnlineBackup : IBackupPlan
    {

        private string bucketName = "kitchen-nightmare";
        private string keyName = "BackupDB.Bak";
        private string filePath = "E:\\BackupDB.Bak";

        public void DumpData()
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };

                PutObjectResponse response = client.PutObject(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }
    }
}
 
  
