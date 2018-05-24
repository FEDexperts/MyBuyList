<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true" CodeFile="Tips.aspx.cs" Inherits="ContactUs" %>
<%@ Register Src="~/UserControls/ucContactUs.ascx" TagPrefix="uc1" TagName="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">

<asp:Panel ID="Panel1" runat="server" ScrollBars="None" Width="95%" CssClass="tipsPanel">
<div style="clear:both;margin-bottom:20px;">
    <asp:Image runat="server" ImageUrl="~/Images/Header_Tips.png" />
</div>

<table width="95%">
    <tr>
        <td colspan="2" align="right" >
           <b>הערות מערכת/ טיפים</b>
        </td>
        
    </tr>
    <tr>
        <td style="width:2px;" valign="top">•</td>
        <td >
            כדי להפוך נקניקיה או קציצת בשר לקלילה יותר, מומלץ להוסיף לה לחם שהושרה במים ונסחט לפני שמוסיפים לתערובת הבשר.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
            	תוספת של כפית עד שתיים אבקת סודה לכל ק"ג בשר או פשוט מי כוס מי סודה קרים תגרום לקציצות להיות אווריריות ופריכות.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
            	על מנת שהרוטב יחדור ויעשיר את המילוי מומלץ לחורר אותו מעט באמצעות מזלג.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
            	האורז ממשיך להתבשל גם לאחר תום תהליך הבישול על האש מהאדים שלו עצמו. <br />
            אין לפתוח את המכסה לפני סיום התהליך,אחרת האדים יברחו ולא יהיו מספיק נוזלים לריכוך האורז. 
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	המלח מקשה את החלבונים שבקטניות וגורם להתקשותן, ולכן יש להוסיף את המלח ממש לפני תום הבישול. 
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	בהקצפת שמנת יש להקפיד שהיא תהיה קרה מאוד. כשמתקבלת קצף רך יש לעצור על מנת לשמור על אחידותה ויציבותה.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	מומלץ ללוש בצק שמרים במהירות נמוכה על מנת שיתפח בצורה האופטימלית.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	בזמן לישת בצק פריך יש לדאוג שהחמאה תהיה על גבול היציבות, כלומר, לא קרה או רכה המידי.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	בהכנת עוגות בחושות יש לדאוג שהחמאה תהיה רכה. 
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	מומלץ להוסיף עשבי תיבול כ – 7 דקות לקראת סוף תהליך הבישול וזאת על מנת לשמר את טעמם.
        </td>
    </tr>
    <tr>
        <td valign="top">•</td>
        <td >
           	מלח יש להוסיף לקראת סוף הבישול כי מצרכים ותבלינים רבים מכילים מלח. <br />
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right" >
           <b> דרכים להסמיך תבשילים </b>
        </td>
        
    </tr>
    <tr>
        <td valign="top">-</td>
        <td >
            רביכה קרה : מערבבים קמח וחמאה ביחס שווה ומאחדים אותם עד שהם הופכים לבצק. מוסיפים לתבשיל. 
        </td>
    </tr>
    <tr>
        <td valign="top">-</td>
        <td >
            רביכה חמה : על השומן (חמאה, שמן) מוסיפים קמח ולאחר מוסיפים את הרוטב.
        </td>
    </tr>
    <tr>
        <td valign="top">-</td>
        <td >
            באמצעות קטניות : אלו סופגים את המים ומפרישים גלוטן ומסמיכים את התבשיל.
        </td>
    </tr>
    <tr>
        <td valign="top">-</td>
        <td >
            באמצעות גרעיני פירות. מצויין בעיקר להסמכת ריבות.
        </td>
    </tr>
    <tr>
        <td valign="top">-</td>
        <td >
           	באמצעות קורנפלור : יש לערבב בכוס קורנפלור עם מעט מי ברז (קרים) . להוסיף לתבשיל.<br />
            
        </td>
    </tr>
    <tr>
        <td valign="middle">
           *
        </td>
        <td>
           	חשוב לדעת קונפלור אינו משנה את טעם התבשיל. קמח כן משנה.
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="נתונים על יחסי המרה של שמרים "></asp:Label>
        </td>
    </tr>
     <tr>
        <td colspan="2">
        חבר'ה, להדפיס ולשמור על יד אופה הלחם או בכלל במקום נגיש במטבח:
        </td>
    </tr>
     <tr>
        <td colspan="2">
        50 גרם שמרים טריים (קוביה אדומה) = 25-30 גרם (כשתי כפות) שמרים יבשים<br />
25 גרם שמרים טריים - (חצי קוביה) = בערך 12 גרם שמרים יבשים, (כף אחת)<br />
10 גרם שמרים טריים = בערך 4-5 גרם שמרים יבשים
        </td>
    </tr>
     <tr>
        <td colspan="2">
        שימרית = בדיוק כמו שמרים טריים מבחינת המשקל, ההבדל היחיד שאין צורך להתסיס אותם מראש בכוס עם מים וכו', אלא מפזרים אותם ישירות לבצק. 
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="2">
            ומכיוון אחר:
        </td>
    </tr>
     <tr>
        <td colspan="2">
            כף אחת שמרים יבשים = 12 גרם בערך, ושקולה כנגד חצי קוביית שמרים או חצי חבילת (25 גרם, שימרית
כמות זו מספיקה להתפחת חצי ק"ג קמח

        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="2">
        להתפחת קילו שלם של קמח, צריך:
        <br />
40-50 גרם שמרים טריים (כל הקוביה או טיפ טיפה פחות) 
<br />
ו/או שקית שימרית, 
<br />
ו/או 2 כפות שמרים יבשים
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="2">
        שני דברים חשובים שצריך לזכור:
        </td>
    </tr>
     <tr>
        
        <td colspan="2">
            1 - שמרים היא 'יצורים' חיים, והם לא אוהבים מלח. צריך להמנע ככל האפשר ממגע ישיר מדי בין השמרים (מכל סוג שהוא) לבין המלח שבמתכון. את המלח רצוי לערבב מראש עם הקמח, כדי שיפוזר ולא יהיה בריכוז גדול כשמתבצע החיבור עם השמרים.
        </td>
    </tr>
     <tr>
       
        <td colspan="2">
            2 - כמות השמרים ותפיחתם בבצק תלויה בהמון גורמים, ובעיק מהזמן שאתם מקדישים לתפיחה, ומהרכב המוצרים האחרים בבצק. ניתן לקצר זמן תפיחה על ידי הגדלת כמות השמרים, אבל מאד לא רצוי לעשות זאת, כיוון שעודף שמרים מקנה לבצק, ולמוצר המוגמר, טעם לוואי לא נעים.
הכלל הוא שעדיף פחות שמרים, ויותר זמן התפחה..

        </td>
    </tr>
     
     <tr>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="2">
        </td>
    </tr>
</table>
</asp:Panel>

<br /><br />
<div style="padding-right: 15px; width:400px;color:#656565;font-weight:bold;">
  <div>
    <asp:Label ID="Label1" runat="server" Text="יש לך טיפ ? שתף אותנו בו..." Font-Bold="true" ForeColor="#656565"></asp:Label>
  </div> 
  <div style="margin-top: 10px; margin-bottom: 24px;">
    <uc1:ContactUs ID="uc" runat="server"></uc1:ContactUs>
  </div>    
</div>
</asp:Content>

