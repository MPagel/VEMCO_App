<?php
require_once substr(__FILE__, 0, -4) . '.behind.php';
use Config\Constants\Session_Variables as Session;
use Config\Database\Mysqli_Connection as MysqliConnect;

$page = new Lib\Views\Page(Session::User);
$page->IncludeCss("/assets/data_query/main_query.css");
//$page->IncludeJs("/assets/shared/js/bootstrap/bootstrap-dropdown.js");
$page->IncludeJs("/pages/email-validation.js");
$page->BeginHTML();


//FOR PRINTING EMAIL ADDRESS IN TEXT BOX, SELECTING CURRENT EMAIL PREFERENCE ---------------------------------
$sql = "SELECT `email_address`, `fish_email_preference`, `shark_email_preference` FROM `members` WHERE `username` = '" .$_SESSION[Session::UserName] ."'";

$db = MysqliConnect::GetMysqliInstance();
$result = $db->query($sql);

$row = $result->fetch_row();
$emailAddress = $row[0];
$fish_email_preference = $row[1];
$shark_email_preference = $row[2];


MysqliConnect::Disconnect();
//------------------------------------------------------------------------------------------------------------
?>

<h3>Email</h3><hr />
<form name="form1" action="email.behind.php" method="get" class="queryForm form-stacked" id="main-form" onsubmit="return validate()">
    <fieldset>
        <div class="query-group ">
            <p>Email:</p>
            <input class="span3" name="email" type="text" value="<?php echo isset($result) ? $emailAddress : null; ?>" maxlength="256" />
        </div>
    </fieldset>
    <fieldset>
        <div class="query-group">
            <p>Fish Email Preference:</p>
            <?php if ($fish_email_preference == 0) {?>
                <label><input type="radio" name="fish-email-preference" value="0" checked/>No email</label>
                <label><input type="radio" name="fish-email-preference" value="1"/><span id="st-switch">Once every 24 hours (daily at 12:00 PM)</span></label>
            <?php } else { ?>
                <label><input type="radio" name="fish-email-preference" value="0"/>No email</label>
                <label><input type="radio" name="fish-email-preference" value="1" checked/><span id="st-switch">Once every 24 hours (daily at 12:00 PM)</span></label>
            <?php } ?>
        </div>
        <div class="query-group">
            <p>Shark Email Preference:</p>
            <?php if ($shark_email_preference == 0) {?>
                <label><input type="radio" name="shark-email-preference" value="0" checked/>No email</label>
                <label><input type="radio" name="shark-email-preference" value="1"/><span id="st-switch">Every Detection</span></label>
            <?php } else { ?>
                <label><input type="radio" name="shark-email-preference" value="0"/>No email</label>
                <label><input type="radio" name="shark-email-preference" value="1" checked/><span id="st-switch">Every Detection</span></label>
            <?php } ?>
        </div>
  </fieldset>
    <button id="query-button" class="btn primary" data-loading-text="Processing...">Submit</button>
    <img class="upload-indicator" src="/assets/shared/ajax-loader.gif" alt="updating..." />
</form>

<div id="results"></div>
<div id="errors"></div>

<?php $page->EndHTML(); ?>

