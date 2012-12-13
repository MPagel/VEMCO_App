<?php
namespace Lib\Data_Query
{
    use Config\Constants\Session_Variables as Session;
    use Config\Constants\Query as QueryConstants;
    use Config\Database\Mysqli_Connection as MysqliConnect;
    
    class Query_Builder_Realtime
    {
        /**
         *
         * @param array $queryString $_GET array
         * @return string sql statement excluding order by, limit
         */
        public static function GenerateEmailQuery($email, $fishEmailPreference, $sharkEmailPreference)
        {
            @session_start();   

            $sql = "SELECT `id`, `username`, `password`, `fname`, `lname`, `account_type`, `email_address`, `fish_email_preference`, `shark_email_preference`";
            $sql .=" FROM `members`";   
            $sql .= " WHERE `username` = '" .$_SESSION[Session::UserName] ."'";

            $db = MysqliConnect::GetMysqliInstance();
            $result = $db->query($sql);           
            $row = $result->fetch_row();
            
            $id = $row[0];
            $username = $row[1];
            $password = $row[2];
            $fname = $row[3];
            $lname = $row[4];
            $account_type = $row[5];
            
            $updateQuery = "UPDATE `members` SET `id` = $id, `username` = '$username', `password` = '$password',";
            $updateQuery .= " `fname` = '$fname', `lname` = '$lname', `account_type` = '$account_type',";
            $updateQuery .= " `email_address` = '$email', `fish_email_preference` = '$fishEmailPreference',";
            $updateQuery .= " `shark_email_preference` = $sharkEmailPreference";
            $updateQuery .= " WHERE `username` = '$username'";
            
            MysqliConnect::Disconnect();
                        
            return $updateQuery;
        }
 
        
        public static function GenerateQuery()
        {            
            $select = "SELECT `vue`.`date`, `vue`.`time`, `stations_records`.`stations_name`, `locations`.`location_name`, `vue`.`frequency_codespace`, `vue`.`transmitter_id`, `fish`.`genus`, `fish`.`species`, `fish`.`ascension`, `fish_details`.`sex` ";
            
            $from = self::BuildFromClause();
            $where = self::BuildWhereClause();            
            $sort = "`vue`.`date` DESC, `vue`.`time` DESC";
            
            $fullQuery = self::BuildFullQuery($select, $from, $where, $sort);

            return $fullQuery;
        }
        
        
        public static function GenerateCountQuery()
        {
            $select = "SELECT COUNT(*) ";
           
            $from = self::BuildFromClause();
            $where = self::BuildWhereClause();
            $fullQuery = self::BuildFullQuery($select, $from, $where);            
            
            return $fullQuery;
        }
                       
        
        protected static function BuildFromClause()
        {
            $fromStatement = "FROM `vue` ";
            
            $fromStatement .= "LEFT JOIN `fish` on `vue`.`transmitter_id` = `fish`.`transmitter_id` ";
            $fromStatement .= "LEFT JOIN `fish_details` on `fish`.`id` = `fish_details`.`fish_id` ";
            $fromStatement .= "LEFT JOIN `stations_records` on `vue`.`receivers_id` = `stations_records`.`receivers_id` ";
            $fromStatement .= "LEFT JOIN `projects_stations` on `stations_records`.`stations_name` = `projects_stations`.`stations_name` ";
            
            $fromStatement .= "LEFT JOIN `locations` on `projects_stations`.`stations_name` = `locations`.`stations_name` ";
           
            $fromStatement .= "INNER JOIN `projects_members` on `projects_stations`.`projects_name` = `projects_members`.`projects_name` ";
            $fromStatement .= "INNER JOIN `members` on `projects_members`.`members_id` = `members`.`id` ";
            
            return $fromStatement;
        }
        
        
        protected static function BuildWhereClause()
        {
            //Set current timezone and set the start/end date -----------------------------------------------------
            date_default_timezone_set('America/Los_Angeles');

            $startDateTimestamp = time() - QueryConstants::Weeks * QueryConstants::Days * QueryConstants::Hours
                          * QueryConstants::Minutes * QueryConstants::Seconds;            
            
            //Replace '/' with '-' in dates
            $dateStart = str_replace('/', '-', date('Y/m/d', $startDateTimestamp));            
           //------------------------------------------------------------------------------------------------------
            
            
            $db = MysqliConnect::GetMysqliInstance();
            $dateStart = trim($db->real_escape_string($dateStart));
            MysqliConnect::Disconnect();
              
            $where = " WHERE (`members`.`username` = '" .$_SESSION[Session::UserName] ."')";
            $where .= " AND (`vue`.`date` BETWEEN `stations_records`.`date_in` AND `stations_records`.`date_out`)";
            $where .= " AND (`vue`.`date` >= '$dateStart') ";

            return $where;
        }
        
        
        protected static function BuildFullQuery($select, $from, $where, $sort = '')
        {
            $orderClause = $sort == '' ? "" : " ORDER BY $sort";
            if (is_string($from))
            {
                $fullQuery = $select . $from . $where . $orderClause;                
            }
            else
            {
                $fullQuery = $select . $from[0] . $where . " UNION ";
                $fullQuery .= $select . $from[1] . $where . $orderClause;  
            }
                      
            return $fullQuery;
        }         
    }
}
?>
