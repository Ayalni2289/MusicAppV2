   M S B A M L   `   `   `  ����   MusicApp&     MusicApp.Payment.PaymentPlatform   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  PaymentPlatform��$	��716��$	��832�� � 	Resources  �� 
TargetType �� Property �� Value   PaymentContainerStyle3  -System.Windows.Media.Effects.DropShadowEffect   Color   Opacity   
BlurRadius  SectionStyle �� Triggers	 P� Property
 P� Value  AddPaymentButton  ActionButton  CancelButton  ProfilePictureStyle @� Center @� RadiusX @� RadiusY��%b  '��       &	  �     '�� 3    &	 v    '�� �    '�� �    &	 �    &	 �    &	 $    &	 M    �� " ��.���  � ��$2 ,Helvetica Neue, Helvetica, Arial, sans-serif)���  � �� �#121212��  � �� �#FFF�� " ��.���  � �� �#191414��  � ��$ 50q���  � ��$ Stretch=���  � ��$ Stretch=���  �@J�Effect   $ Black��$	 0.5Q�$ 10Q��� " ��.���  � ��$ Center=���  � ��$ 0,0,0,20q��� " ��.���  � ��$ 0,0,0,30q��� " �� P� 	 � ��$

 Bold"�. ���  � �� �#9F61E2�� " ��.���  � ��$ 10,20q���  � �� �#1DB954��  � �� �#FFF��  � ��$ 0q���  � ��
 ���� ��  �@�Cursor$
 Hand�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#198754�� " ��.���  � �� �#28A745�� " ��.���  � �� �#28A745�� " ��.���  � �� �#DC3545�� " ��.���  � ��$	 100����  � ��$	 100����  � ��$ 0,0,0,10q���  � ��$ UniformToFill=���  �@J�Clip @� $ 50,50&�$ 50Q�$ 50Q�.���  � ColumnDefinitions �� $
��532* ��� $
��141* ��� $
��127* �.+��� #��[  $��0,0,0,0q�$��3��.+��� $$��Payment Information Management��$��36(�$
��Bold"�$��Center=�$��	0,20,0,20q��� #��[ .+��� $��10q��� #��[ .+��� $��  Subscription Management��$��18(�$
��Bold"��� #��[ $��Images/profile_pic.jpg��$��Center=��� $��  Current Plan: Premium��
��������� $*��$  Next Renewal Date: January 1, 2025��
���������  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   #��[ $��Manage Subscription��$��5q�$��38��$	��140�� � Cursor$
 Hand���� -   #��[ $��Cancel Subscription��$��5q�$��38��$	��130��$
 Hand���� #��[ .+��� $��  Receipts and Invoices��$��18(�$
��Bold"��� $��10q�.+��� $��January 2025 Invoice��
��������� $��December 2024 Receipt��
��������� -   $��Download��$��10q��� #��[ .+��� $��	  Support��$��18(�$
��Bold"��� $0��*  Contact our support team for assistance.��
��������� -   $��Contact Support��$��10q�