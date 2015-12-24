;#DropDB Solution1EasyTest

#Application Solution1.Win
#Application Solution1.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef Solution1.Win

*Action About...
 
*Action OK

#EndIf

#IfDef Solution1.Web
*Action Navigation(My Details)
#EndIf
