<?php
set_include_path($_SERVER['DOCUMENT_ROOT']);
spl_autoload_extensions(".php");
spl_autoload_register();
use Lib\Data_Query\Query_Builder_Realtime as QueryBuilder;
use Lib\Data_Query\Query_Process as QueryProcess;


if (isset($_GET['email']) && isset($_GET['fish-email-preference']) && isset($_GET['shark-email-preference']))
{   
    $emailAddress = $_GET['email'];

    //if email address matches regular expression, update email to database.
    if (filter_var($emailAddress, FILTER_VALIDATE_EMAIL))
    {
        $updateSql = QueryBuilder::GenerateEmailQuery($_GET['email'], $_GET['fish-email-preference'], $_GET['shark-email-preference']);       
        QueryProcess::UpdateEmail($updateSql);
        header("Location: /pages/data-query/main-query.php");
    }
    else //Else refresh page
        header("Location: /pages/home/email.php");
}

?>
