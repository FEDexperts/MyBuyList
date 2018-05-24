<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WelcomePage.aspx.cs" Inherits="WelcomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        tr
        {
            height: 30px;
        }
        p.MsoNormal
        {
            margin-bottom: .0001pt;
            text-align: right;
            direction: rtl;
            unicode-bidi: embed;
            font-size: 12.0pt;
            font-family: "Times New Roman";
            margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
        .style1
        {
            border-collapse: collapse;
            text-align: right;
            font-size: 10.0pt;
            font-family: "Times New Roman";
            border: 1.0pt solid windowtext;
        }
    </style>
</head>
<body dir="rtl">
    <table style="text-align: right; font-family: Arial (Hebrew)">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="משתמש חדש שלום,"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblText1" runat="server" Font-Bold="true" Text='תודה שנרשמת לאתר והצטרפת למשתמשים הרבים שאימצו את המערכת לשגרת חייהם ובחרו בדרך ה"מטעימה" לארגון הארוחה.'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblText2" runat="server" Font-Bold="True" Text="מערכת"></asp:Label>
                <b><span style="color: #C51015">My</span>BuyList</b>
                <asp:Label ID="lblText3" runat="server" Font-Bold="true" Text="פותחה ככלי מסייע בתכנון ארוחות וקניות המאפשר שימוש במבחר רב של מתכונים ממקורות מגוונים."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblText4" runat="server" Font-Bold="true" Text="שילוב המערכת כחלק בלתי נפרד מתרבות ניהול משק הבית תאפשר חסכון ומימוש חוויות קולינאריות."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="קבלת מייל זה מאשר כי הדואר האלקטרוני שנרשם במערכת מדוייק, ומעתה תוכל לקבל את בחירותייך ישירות לדואר האלקטרוני."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblText6" runat="server" Font-Bold="true" Text="בברכה, <br/> צוות האתר"></asp:Label><br />
                <asp:HyperLink ID="lnkCustomerService" runat="server" Text="customerservice@mybuylist.com"
                    NavigateUrl="mailto:customerservice@mybuylist.com" />
            </td>
        </tr>
        <%--<tr>
            <td style="color: #99CC00;">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text=" המסלול"></asp:Label>
                &nbsp;
                <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Italic="true" Font-Underline="true"
                    Text="לשימוש במתכוני המאגר ושיבוצם "></asp:Label>
                <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="בצורת ארגון כלשהי . "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <table border="1" cellpadding="0" cellspacing="0" class="style1" dir="rtl" style="mso-yfti-tbllook: 480;
                        mso-padding-alt: 0in 5.4pt 0in 5.4pt; mso-table-dir: bidi">
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Image ID="Image1" runat="server" ImageUrl="http://www.mybuylist.com/Images/step1_1.JPG"  />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">בחירת צורת ארגון – פתקית
                                        &quot;תפריט לארוחה&quot; או &quot;ארגון תפריט שבועי&quot; או &quot;ארגון תפריט חודשי&quot;<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                               
                                    <asp:Image ID="Image2" runat="server"   ImageUrl="http://www.mybuylist.com/Images/step1_2.PNG" />
                                           
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle">
                                
                                        <asp:Image ID="Image3" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step2_1.JPG" />
                                        
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">בחירת מתכונים
                                        <o:p></o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">ע&quot;י סימון.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                            </td>
                            <td valign="middle" width="189" align="center">
                                <asp:Image ID="Image10" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step2_2.JPG" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" width="189">
                                <asp:Image ID="Image4" runat="server" ImageUrl="http://www.mybuylist.com/Images/step3_1.JPG" />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p class="MsoNormal" dir="RTL" style="vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p class="MsoNormal" dir="RTL" style="vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">שיבוץ/ארגון <span style="mso-spacerun: yes">
                                        &nbsp;</span>מתכונים – ע&quot;י גרירה.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                               
                                        <asp:Image ID="Image5" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step3_2.JPG" />
                                          
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" width="189">
                                <asp:Image ID="Image6" runat="server" Height="71" ImageUrl="http://www.mybuylist.com/Images/step4_1.jpg" />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">הפקת רשימת קניות ושמירת
                                        מערך ארגון.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                              
                                        <asp:Image ID="Image7" runat="server" Height="121" ImageUrl="http://www.mybuylist.com/Images/step4_2.JPG" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="color: #99CC00;">
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text=" המסלול"></asp:Label>
                &nbsp;
                <asp:Label ID="Label10" runat="server" Font-Bold="true" Font-Italic="true" Font-Underline="true"
                    Text=" להוספה של מתכונים אישיים למאגר המתכונים + שימוש במתכוני המאגר ושיבוצם  "></asp:Label>
                <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="בתפריט כלשהו. "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="שלב 1 -  בחירת פתקית &quot;הרשימות שלי&quot;  "></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image8" runat="server" ImageUrl="http://www.mybuylist.com/Images/step1.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="שלב 2 -  בחירת האפשרות &quot;רשימת המתכונים שלי&quot;"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="שלב 3 -  בחירת האפשרות &quot;מתכון חדש&quot;"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image12" runat="server" ImageUrl="http://www.mybuylist.com/Images/step2.jpg" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="שלב 4 -  הכנסת מתכון חדש"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image9" runat="server" ImageUrl="http://www.mybuylist.com/Images/step4.jpg" Width="200px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="המשך..."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                   <div align="right">
                    <table border="1" cellpadding="0" cellspacing="0" class="style1" dir="rtl" style="mso-yfti-tbllook: 480;
                        mso-padding-alt: 0in 5.4pt 0in 5.4pt; mso-table-dir: bidi">
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Image ID="Image11" runat="server" ImageUrl="http://www.mybuylist.com/Images/step1_1.JPG"  />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">בחירת צורת ארגון – פתקית
                                        &quot;תפריט לארוחה&quot; או &quot;ארגון תפריט שבועי&quot; או &quot;ארגון תפריט חודשי&quot;<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                               
                                    <asp:Image ID="Image13" runat="server"   ImageUrl="http://www.mybuylist.com/Images/step1_2.PNG" />
                                           
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle">
                                
                                        <asp:Image ID="Image14" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step2_1.JPG" />
                                        
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">בחירת מתכונים
                                        <o:p></o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">ע&quot;י סימון.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <span lang="HE" style="font-family: Arial">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                            </td>
                            <td valign="middle" width="189" align="center">
                                <asp:Image ID="Image15" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step2_2.JPG" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" width="189">
                                <asp:Image ID="Image16" runat="server" ImageUrl="http://www.mybuylist.com/Images/step3_1.JPG" />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p class="MsoNormal" dir="RTL" style="vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p class="MsoNormal" dir="RTL" style="vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">שיבוץ/ארגון <span style="mso-spacerun: yes">
                                        &nbsp;</span>מתכונים – ע&quot;י גרירה.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                               
                                        <asp:Image ID="Image17" runat="server"  ImageUrl="http://www.mybuylist.com/Images/step3_2.JPG" />
                                          
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" width="189">
                                <asp:Image ID="Image18" runat="server" Height="71" ImageUrl="http://www.mybuylist.com/Images/step4_1.jpg" />
                            </td>
                            <td align="center" valign="top" width="189">
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">הפקת רשימת קניות ושמירת
                                        מערך ארגון.<o:p></o:p></span></b></p>
                                <p align="center" class="MsoNormal" dir="RTL" style="text-align: center; vertical-align: top">
                                    <b><span lang="HE" style="font-family: Arial; color: #993300">
                                        <o:p>&nbsp;</o:p>
                                    </span></b>
                                </p>
                            </td>
                            <td align="center" valign="middle">
                              
                                        <asp:Image ID="Image19" runat="server" Height="121" ImageUrl="http://www.mybuylist.com/Images/step4_2.JPG" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server"  Text="רשימת התפריטים שהמשתמש הכין נשמרים באופו אוטומטי ע&quot;י המערכת. על מנת, שהמערכת תוכל לאחזר (לשלוף) במהירות, כל פריט מידע, יש לתת שם משמעותי לכל רשימה ורשימה. "></asp:Label>
            </td>
        </tr>--%>
    </table>
</body>
</html>
