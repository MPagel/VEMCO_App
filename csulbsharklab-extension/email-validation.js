/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */    

function validate()
{ 
    var email = document.form1.email.value;

    var regExp = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/i;
    
    if (regExp.test(email)) {
        alert("submission successful");
        return true;       
    }
    else {
        alert("invalid email");
        return false;
    }                   
}