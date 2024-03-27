/*
SQLyog Enterprise - MySQL GUI v6.13
MySQL - 5.6.17 : Database - istlbd
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

create database if not exists `istlbd`;

USE `istlbd`;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `lookup` */

DROP TABLE IF EXISTS `lookup`;

CREATE TABLE `lookup` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Code` varchar(10) DEFAULT NULL,
  `Name` varchar(30) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `ParentId` bigint(20) DEFAULT '0',
  `Status` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;

/*Data for the table `lookup` */

insert  into `lookup`(`Id`,`Code`,`Name`,`Type`,`ParentId`,`Status`) values (1,NULL,'Dhaka','PlaceOfBirth',0,1),(2,NULL,'Savar','PlaceOfBirth',0,1),(3,NULL,'Bangladeshi','Nationality',0,1),(4,NULL,'Dual Nationality','Nationality',0,1),(5,NULL,'Male','Gender',0,1),(6,NULL,'Female','Gender',0,1),(7,NULL,'Division 1','Division',0,1),(8,NULL,'Division 2','Division',0,1),(9,NULL,'Division 3','Division',0,1),(10,NULL,'District 1 (Division 1)','District',7,1),(11,NULL,'District 2 (Division 1)','District',7,1),(12,NULL,'District 3 (Division 2)','District',8,1),(13,NULL,'District 4 (Division 2)','District',8,1),(14,NULL,'District 5 (Division 3)','District',9,1),(15,NULL,'Station 1 (District 1)','Station',10,1),(16,NULL,'Station 2 (District 1)','Station',10,1),(17,NULL,'Station 3 (District 2)','Station',11,1),(18,NULL,'Station 4 (District 3)','Station',12,1),(19,NULL,'Upazila 1 (Station 1)','Upazila',15,1),(20,NULL,'Upazila 2 (Station 1)','Upazila',15,1),(21,NULL,'Upazila 3 (Station 2)','Upazila',16,1),(22,NULL,'Upazila 4 (Station 3)','Upazila',17,1),(23,NULL,'Union 1 (Upazila 1)','Union',19,1),(24,NULL,'Union 2 (Upazila 1)','Union',19,1),(25,NULL,'Union 3 (Upazila 2)','Union',20,1),(26,NULL,'Union 4 (Upazila 3)','Union',21,1),(27,NULL,'PostCode 1 (Union 1)','PostCode',23,1),(28,NULL,'PostCode 2 (Union 1)','PostCode',23,1),(29,NULL,'PostCode 3 (Union 2)','PostCode',24,1),(30,NULL,'PostCode 4 (Union 3)','PostCode',25,1),(31,NULL,'Application Status 1 ','ApplicationStatus',0,1),(32,NULL,'Application Status 2','ApplicationStatus',0,1),(33,NULL,'A+','BloodGroup',0,1),(34,NULL,'B+','BloodGroup',0,1),(35,NULL,'O+','BloodGroup',0,1),(36,NULL,'AB-','BloodGroup',0,1);

/*Table structure for table `personenrollment` */

DROP TABLE IF EXISTS `personenrollment`;

CREATE TABLE `personenrollment` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `FirstNameEn` varchar(50) DEFAULT NULL,
  `MiddleNameEn` varchar(50) DEFAULT NULL,
  `LastNameEn` varchar(50) DEFAULT NULL,
  `FirstNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `MiddleNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PlaceOfBirth` tinyint(4) DEFAULT NULL,
  `NationalityId` tinyint(4) DEFAULT NULL,
  `DateOfBirth` date DEFAULT NULL,
  `Gender` tinyint(4) DEFAULT NULL,
  `MotherName` varchar(100) DEFAULT NULL,
  `FatherName` varchar(100) DEFAULT NULL,
  `SpouseName` varchar(100) DEFAULT NULL,
  `MaritalStatus` tinyint(4) DEFAULT NULL,
  `Occupation` tinyint(4) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `MobileNumber` varchar(20) DEFAULT NULL,
  `PermanentDivisionId` tinyint(4) DEFAULT NULL,
  `PermanentDistrictId` tinyint(4) DEFAULT NULL,
  `PermanentStationId` smallint(6) DEFAULT NULL,
  `PermanentUpazilaId` smallint(6) DEFAULT NULL,
  `PermanentUnionId` mediumint(9) DEFAULT NULL,
  `PermanentPostCode` smallint(6) DEFAULT NULL,
  `PermanentAddress` text,
  `PresentDivisionId` tinyint(4) DEFAULT NULL,
  `PresentDistrictId` tinyint(4) DEFAULT NULL,
  `PresentStationId` smallint(6) DEFAULT NULL,
  `PresentPostCode` smallint(6) DEFAULT NULL,
  `PresentAddress` text,
  `Status` varchar(255) DEFAULT NULL,
  `ApplicationStatus` tinyint(4) DEFAULT NULL,
  `BloodGroupId` tinyint(4) DEFAULT NULL,
  `CategoriesId` varchar(255) DEFAULT NULL,
  `OrganizationId` varchar(255) DEFAULT NULL,
  `Designation` varchar(255) DEFAULT NULL,
  `Remarks` text,
  `Photo` longblob,
  `PhotoUrl` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `personenrollment` */

insert  into `personenrollment`(`Id`,`FirstNameEn`,`MiddleNameEn`,`LastNameEn`,`FirstNameLocal`,`MiddleNameLocal`,`LastNameLocal`,`PlaceOfBirth`,`NationalityId`,`DateOfBirth`,`Gender`,`MotherName`,`FatherName`,`SpouseName`,`MaritalStatus`,`Occupation`,`Email`,`MobileNumber`,`PermanentDivisionId`,`PermanentDistrictId`,`PermanentStationId`,`PermanentUpazilaId`,`PermanentUnionId`,`PermanentPostCode`,`PermanentAddress`,`PresentDivisionId`,`PresentDistrictId`,`PresentStationId`,`PresentPostCode`,`PresentAddress`,`Status`,`ApplicationStatus`,`BloodGroupId`,`CategoriesId`,`OrganizationId`,`Designation`,`Remarks`,`Photo`,`PhotoUrl`) values (1,'Mr.','Al-Amin','Chowdhury','????? ???????',NULL,'????? ???????',1,4,'1988-01-30',5,NULL,NULL,NULL,NULL,NULL,NULL,'01711390910',7,10,15,19,23,28,'Flat B2, House 285, Road 4, Mirpur DOHS ',NULL,NULL,NULL,NULL,NULL,NULL,32,34,NULL,NULL,NULL,NULL,'ÿØÿà\0JFIF\0\0`\0`\0\0ÿá\0ZExif\0\0MM\0*\0\0\0\0\0\0\0\0\0\0\0J\0\0\0\0\0\0\0\0Q\0\0\0\0\0\0\0Q\0\0\0\0\0\0ÃQ\0\0\0\0\0\0Ã\0\0\0\0\0† \0\0±ÿÛ\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342ÿÛ\0C			\r\r2!!22222222222222222222222222222222222222222222222222ÿÀ\0\0ü\0È\"\0ÿÄ\0\0\0\0\0\0\0\0\0\0\0	\nÿÄ\0µ\0\0\0}\0!1AQa\"q2‘¡#B±ÁRÑğ$3br‚	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹ºÂÃÄÅÆÇÈÉÊÒÓÔÕÖ×ØÙÚáâãäåæçèéêñòóôõö÷øùúÿÄ\0\0\0\0\0\0\0\0	\nÿÄ\0µ\0\0w\0!1AQaq\"2B‘¡±Á	#3RğbrÑ\n$4á%ñ\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz‚ƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹ºÂÃÄÅÆÇÈÉÊÒÓÔÕÖ×ØÙÚâãäåæçèéêòóôõö÷øùúÿÚ\0\0\0?\0õÍ£(Ú=)h« 0¾”›G ü©h Ú¾‚£Óô¥¤ ã€G$¹›yè{{\n“Óô¢Š	è( zÊŒÒf˜è?*L/  ¶*­Æ£ihÁn.bˆ˜\n\0µéI´z\nÈoh©&ÆÔ­÷{=h[ŞÛİÇæ[Ì’§ª04cÓô£è)3J(p¹è)vŒô•7<Ó¨\0Ú=åF ¥´€nÕÏAK±}åKK@\rØ¾ƒò¤Ø¿İú(¸È£iÀ¢Ÿ\'Ü4S@-QHŠ( ¢–’€\nBh5›¬k6š-£\\^IµG@:±ô¾HêÖ©ed…®.#@:ä×’ëÿ\0.õe·?g·\0€ò~¦¸Û­râbK¶IõëùÒæ-C¹éş#ø†Œ\Zk…\\¬#×›Şj³ß\\’á˜œ±jÂîGäœ(¨Äì©ş*«\\»$k5ÈË6yÏj¹a¯ê:lâK[‡‰º§­sÈû#¨ëÍYÜ²ÆJ±İŒiØV¹éZ_Äûø¤Œ_¬rÅœ9†Ç¨÷¯AÒü]£ê†ò(Üğc•Â°üëçÅ	çØÔ‘]Iñl\ZBq>¦G)„´ñÒ¼;Â^<›D‘mç[G#+SÔğ¯dÓ5K]VÍ.¬åY#oCÈ>‡ŞÌÚ±|Rö¦ƒKHC…-%-\0QE \Zÿ\0pÑCıÃERh¤¥¤QKI@	E‡Ò€!º¹ŠÖŞIæp‘F¥™‰èxg<JÚæ¢Íãm©ÈŞ·~%øŠ{DèÖòl¶„.ßãn¸ú\nòû»†fÈÇ@\r\'©´#dM½B¢Çïdÿ\0*·‹y,B„ƒÓ5{ÂúBİOçJ2£·­zV¨¨\0\0é\\Õ+ò»#º•ÕäyŒzÔ‹–ˆ~”öğåÇ&MzšZ!¨©£³E?pV.†ŞÂš<…ô¨úÆØöÒ.ÓFéé^ĞÖP¸ÉŒëŠ«.•íıj–&]IöÏ\"kÔ\0Ñj©u‘\rÆ6\\C^».“#úVV¥¢Å<»=8«%Ü\Z/cÌá˜ó~÷Zêü1â[\nôO‡‰#\'‡ãùW3hÖWl‡ƒŠXVÀµÖšzœ2…™ôÎ‰­Zë–+ulÇOU>•«šğøŸûTáÏÙf_Œı\r{u•äö©qnáâqjy+2Ğ¥¦Šp¤HRÒRĞ_î\Z(¸h¦€){ÒRÒ\0¤¥£µ\06¡¹™-íäšC„K1ö5=s9•¡ğf¨Ëœ˜¶şdëCØkVx£¨Mw©\\M#’Ó9bsêsYë–l¤ğ)&l¾zUÍ8¸\\Ç&¢NÈê¦¯+Îl ¶DQÚºXcÍbéJJqÖºqÀ¯*nò=Yh‹1Å*qµ:1Ò¦Ïµm+ó2,)]ªp}¨ïUÈ…vR–jÏ¸ƒ*EmÉ‚*œÊ\nÔ8[a©Uâ«\0¬Ì9È#”`GåŠôïÛ‡‰í^rö×Œpq]t–1ÄÅ^å¸› ã#?08é^·ğ¿^DúTÎ ¾!O­xÜyVPÙluæ»_†Òy~+´ùî,:tàó](áÇ½\nu ëJ)‹KIK@\r¸h¡ùCE4\0)i)i\0QIE\0®iË«h—–@óâ*	ècùâ´)¥[Ÿ*_B`¹’&Æäb¥^Ğ yîBĞäÓ5‹I Õ.Rua\"ÈÁÁó]„,÷DÒy8æ°«+@í ¯3¨´‹b*[vêvç®vî÷ÉİA·/j©ı·¨Z…åŠÿ\0xŒâ¸ãNîìîœÎù#aÛı„ué\\=·‹nZEWäg®+©±Õ…ÊüÇ’x­4NÄZV¹¡·ŠaV¤i.k2óZ‚ÕIcœu¦ì„®Ë¬½S¸p +\rüm$, ƒş×5J_½Á&(~Q×p¡Âë@½¢ë4ö²m?7Q^cutĞM…ÏÊzë^©\rÂ]Å¸wJóŸéâÛQ$}×ù­(èìÉ®¯™>{´á^‡ğ¾ĞËâx\\©\Z3çÔãÖ¼öÚ0ÌI>ßZúÀZ\né^¶–Dâe.O|6Öyózp¥‚œ)‹KIKÚ¸h¡şá¢©\0RÒRÒ\0¤íE-\0%!éKHzSãŸtØ³s5º2ÊU^x=²?*OD©¤£‚I®Æ¶Áïgà~ò?\0k\'ÃÑmÑ­ıÁşuÃVNÍ¬ ’‹]QSP™m_(?xÇ–ÆOà;šŠkãaû»«	ZCšs*‚8Î3úVÅÅ†éD«÷ÁÊŸCI¨XZk	ö…›´‘+Æp~••9G©¼ïot¥ÛL¼¦Œ±#\rĞàààò>•©gqÜ\r¹Çj³ä¤º|vPÛˆ`:b„‹l¨?»ßÖ¦i-P&Üu4eR!ÏlW1sl·¾ì‘‚ºi‰6Øö¬¿³4°´hv1<°‘U-X¡¡Í\\Úé–q™.YU»*FXŒz‘LöÄD\ZİC)şˆ¦}=ı«{X¶µ¿Ób´Y\ZÎh	\nÑƒŒ†¡ÁR;;[=Kw»–s™Áà¯­VÜ–äå±ŒqJâhşPG\"¹¯@<ÈXMuÚe›AR+Å–i¸³Šÿ\0*¨?xš‹¢)ü8ğ¹Ö5Ñqsû%®$l¿…Ï¥{Ò*¢ª¨@À¼çÀQI½s\n[¥¸ù@ï‘Ïó¯H®˜K™\\óñıœùE\n-Y€RÑE ÿ\0pÑJÿ\0tÑT„%-%.)\0”´”½¨(¢Šr~.·áœŒ‚Œ‡úW?£„şÎˆ\'İ\\Ô×y¬Ù}»OxÇŞ_™sê+Î´d[›w1ÊxôÍrV¬ôhT½5ÇF«(â¦ª:Ê¢…¾QÍX\nO5Œ7¹RA=ª«`I€9«wCµJ™2O&•E©QÚå¦ÿ\0sô¨-°ÌGqZ\r˜p+$¹‚}àü¹Áªq°“¾ÅÉ-£üèáQ›‡*€{Õµmè¤\'Õ4…vQ’ Šp+™Öòom°2P3ÿ\0*ê\'}ÜW!s)º×æ_	\ZmÀê}E[/ßW;OÛó{tËË2¦~ƒ?Ö»1Yz\rØt˜ca‰o©­Jë‚åŠG›‰©ÏQÉ-%-Q€´QŞŠ\0kıÃE÷\rĞQ@‚Š( ¢–Š\0C\\¦µ¤[Yİ›ÛtØÓœHBGzêë;ZƒÏÓßz?˜TTâkJ\\²9˜\r^¬ÈŸkcÒ­	†Fk†ÎÇ§Òã¦MÊ}ëK{ó~®%	ôP3»ë[M0<\nj¡c»n7`§mŒë‰µ.xá‡?¥WÓ­ï¥‹eÓçïcÖÓDØ9Zb6Æ9ãëMÄKnZˆã‚’\\m5˜Ö£–L\'Zè-õ*ÉË‘øU\rxBKké/ïÀòTÈ?LÑ¥Ãö½J5ÇÊqú\ní…oJ\Z]œµê¸»!E-VÇ´¢’ŠBŠJZ5şá¢‡û†ŠhAE%\0QE%\0-”S\0¦°¤„`ÒÒ3\02N(Âêp›÷?.r>•UîI_”óV|Cv—sÇq1H™Fõ\0‘šÆ‰‰~µçÕ¤ìzÔà®Jÿ\0lß½e\0zmÎ([mFäû`l	qùU¥ëÀªíı û•?•(Ill´Ø{AªÈùËòğ	ñPM&¥òşÑÍşéÈühiõ>èjX–M»¤ûÕ£i!7}ÇCu2€³cpî:\Z’k @ªw°õZ9“íÕ1M²ZI\\îü1hVºùiÂı=k¢ÍaiZ´Fùô—\n“E<c?}ëøş•¸\rw%ecÈ©.i\\u”P@áKM¥ ¢’–€şé¢‘şá¢š\0¤¢“4\0´”™æ“4\0ìÒfšMbk^+Ò´5asp\Z`2!—?áøÓpµy×<d¹“L°“‘4ªzŸîëX:ßÄ‹ûô’+0--Øcå9r>½¿\náè±ëúÓH{Íkaı£á\r>H¹’(CpG\"¹ÙQà|óÅt5»ğÜqƒ–·v¿=Ãô?¥_×t`Û®b\\©åÔãú×=zzó#¯\rZŞë9›}ITÜ}kM58‚ƒÅbMbÀî^G¥W1•èå}s$º­\\é´ctNâö1œX;¤y£å]çï&­Fû’Ñ-ÄæfÂtõ«:M±ŸP‚%–lŸ ëUÌb8÷?\nk¯ğÆ’Ö¶­}sI¦_‘r‰Øs×ÿ\0ÕWyYV\\»9]M¥xO½µ”Ç*Å´şÉÿ\0ë×}áÛøLYã!gL,Ñg•?àkÌ~(Mÿ\0‹TÁO±$ÿ\0…rš/ˆïtïµYKµÈÃ2¬=®Ë[>—\rKšó-âÍ¤åaÕmŒx2Ãó\'â:Ö»ë\rZÇT‡Í±»ŠtõFÎ>£µM…sBŒÓ\nPi\0üÑMÍ-\0÷\rÖû¦Šx¤ÍE5ÔVñ&tVfÀÈët»\0Ék›¹^óÿ\0\n`v[«YñN•¢\'úMÀi{EËõ¿\Zòmkâ­¨nE›Èˆÿ\0<~g©®>{¹gbÌäç’I¦î»ñ?Q»İ\rŠ‹HÏ”åÈúö®{©n¦&Ggf9$œ“P3dòjX“	¼ZªÀ+ÈBT;ÎG5,‹•ªİ3LG¤|2Õ¶ªlİÀépy¦E{\Zó_3é’ZİC4d	#pÈ}äWÑºeâ_iö÷Q‘¶dÇlŠMÌÕ4`¤ÏnŸ!å»î+\nKUaÊ‚+¾ã‡ªi›wO\0ùz²úŠâ«IüQ;hWû29Óàİş¬TŸgHÓ!@®’:çŠšÇLmZl9\"Õ$`~÷û\"¹£Í\'duÊJ*ìAÑÿ\0´®Eäéş‰f5?òÑ‡ ıOÒ»7bikjaT\0ªº…ÊÚYO;–4,^…8r+eZ®¤®xoÄ‘sâ{¢‡*Œ~Ï5Çäv5¥«\\îd›ø‰$ŒõäÖLXä’}É­’3l•Û“j·g©\\ÚÊ²[Ìñ8èÈÄYá¹Å\"ØNÂ¹é:GÄí^ÏtËvƒşzŒ7ıô?­vúgÄ½\Zğ*Ü‰-\\ÿ\0xn_Ì…xB±Ï©RFÈ4¹@úvÏS²¿MÖ—PÌ?ØpqV÷\nùßPÚEx¥taÑ”àŠêtßˆzİU7^zÓ.ï×¯ëK”ssò\Z+†Ğ~!Ûê×YÜ[§”…VFÊ“üÅ’a¬x“PÕeİspòsÀ\'ô+¥v9Ï4¬‡u&ÊBÙ=é„qS¤`j ~d¸Ç­Ytæª¶ôù¢lCĞÔ‘_#“-ûg¡úS÷L­VdÇZ¸Î {úUfå³@	+0¯qøq¨ÛË@çcÏóÍx`??ã^‹ğËSkMlÇ<xQÈş´4>‡²µÍİøÇO:ƒi–76ÒŞôÃ¾Lÿ\0ö“âéºf ºL“0,?$|ìøxäúW<şÓµ¨şÛ¢]*±•7n×ªŸóŠÌiN§sı•mö»!.KË,R ÷ùqÅQ³ñ\rÎ¡Î•u¡‰0,0T{ç9#¢²¬<I©xrãû3[ŠGxËºP‰ÏµI®èQù]ğü€\0<Ã]=Êÿ\0U¥Ê–¨Ú÷ÒGK¡xÍ/¯—©Â,µ1Ñ3”“ıÓı*O^_]|Ø2üMxŞµ­¾¹ÔÆ>ŸÌVïˆuÛûïé–ú‡ü|d–p~ø\0Ÿ~j’2”Rz]×ÎIãşª½) “ÍV`	ŒJĞÌ‡ŒiÇ‘JéÚ˜]„›K>:cùúP2X‡Ês‘ÛÖ¬PÅÜü·°Æ*À^1Ò“\04õûÜQúÓ—­\0t$xŸMç=h§xHøIôßúî´P‰hÈeäÓv\Z²G=)„cš“B³¨;š°øÅ@Àƒ<â˜0çšoÙUşøÊúzÕ¯\'iÉëJHÇ­ *àvİ¼š›\0“Ò‚¸¦!y­\r>òm>î9íä)*6Uı*¦\r)€/ÜxZæûN¸¿óe–õšEbIqş=ê‡†uİKÃúš5µÁPFJU¿ŞéŞ5¢]çjaïkŠñF”š7ˆb½Œ*ÙÜ¹ÂŠİı±Eµ&mY™iªhş<ÓäbÅÙŸ™O÷÷ßp×sêŞÕÚÊá‹ØÜ‚>ë´=¨ÿ\0ë\ZÌ¸·–Íã¼±‘ÑĞîR‡‡ÔVÊëËãk8¬/‘Eä®Ì:0’=28?Z\\­;”ä­©GMĞ–]]Ë//šÀt=(ñ©S‚ÿ\0,à©?à+·Ò,D{åaË…y÷‹gó¼IyˆB~CüsL˜İî`°\" )ÇµOÓÒš}©Œ®S<ôæ–XÓnÑ×’{š”¨\'·áHHÚçJzb¢”ñ’3Ò7×ùPÈüjD¨©ŸN(¢ğ—üŒúoıwZ)¾ÿ\0‘ŸMÿ\0®ëüè¦ˆ–åB2j3×â})õ¨5!j§0!²*ã\Z‚\\Å0}Ëšıj›T½LON@¥­4P3ƒL\0œf)NOJgs@ç„oh¥3óDì¼ş:©ñÖK*ÏÊƒÌuå¾îx\0óŸÓƒ¥jãK´¾ó>îÑ ü8?ÌVAñ-ÅåçÛofq\n6c„ÿ\0_Ş›W3Šw;!¥YéY¯/§má#+Ëz\0EcxBM¾\"‘å-¹£m­JËşëÄw…ä88UÏ=½Ou{‘}jñHp=õïB]Yr×D{E¦>Ë»Ú¼gQ¹—÷#¤²³¡9éqj¡<\'uyV´û‘ÇëŠò§ J]B;\nÒEV\0…<*Äƒ»¶Ÿ¥\'oZ7$Òâ“æÜp3øQ(\0İ©cĞ{Ô036dbrÜóM»“,®~lzT¨¸@\0¦Á³OCÍW¥Fæ‘ÒxLãÄúoıw_çECágÿ\0ŠŸLÁÿ\0—„ştS‰3Z•#|Œ£ƒN\'åæª)rG@ÜÕ€ÙZƒA‡­G\"‘‘Ş¤=ê2294À©  î¥Fêid´÷¦Ia=OJ3Í\n¦‚¼s@ÄŸLñHÂœëJÃœÒ3TlÛF#ÔœV,’y®¸U5ÑÜÆdĞ Šæ‚°ÁŒS)ltºTÿ\0e‹Ëˆ\rì8éÇ¹®ÛGğ—Ã:–·¨ä+[¿’¿/şµGğßÁSÙ¨ê@ı‘NR29ûÿ\0³üë_Æş#:»hã|E‚;GÑØtQÛhî}½ª%+è	ÌÚÔcÃ¦dùU€ÿ\0duşCó®}E±Æ£2±Éˆóã­K´?­hÙVVŒŠNßJ~09¦ö¤1­Á=i²6ÕÜz\nyZÏÔ%ÀƒË{PnÍ=ËLF=jşMW·ŒF¡HøÍNÀàzS`iû±ïøT\0ÿ\0	ştænƒšL7ü Kx¯Lÿ\0¯…ştQàá·Äšiõ¹_çE(ìMM†uÖD™•=ªÔLYA­	tÈ7u~¾¢¤ƒO„eÿ\01XªŠæÎ&iJŒ©­³§Âåÿ\0:oöt<rÿ\0W2ŒB‡¥C,$óé[ÿ\0`‡wVüèãø¿:9Ã”Ã„3}j]œÖ²ØB­•.;u©¿³áå¿:ÂÆ—Ï­!Œô­±§ÃÏ-ùŠÂİºúÓæ\rMMáï¾¯}4¯•·ˆäñ×ØV¿Ø!ÿ\0kó«6Ï-•”ñ[Lñ¬Çk•<ã½(çC³KBÅß‰î\r·ö–AıÛ¼|–í±qúÖî£ÛøOE›TÔ6ı°Ç¿s?À¾¤÷ÿ\0\n¯à-\"Ñ/å»ØZT%SqÈ_§½Aã‰$½Ö\ZÖYÈ‡N ~¼ÒrÓ˜›]òœfY%œ®²§>•¸4èm]Ø©¿a‡?ÅùÑÎW)å,±o›#i»:VïØ!ÿ\0kó¦(}èç)„ÉYF÷Í €:q]|¶•?{óªé¦Û©\rÏ½\'SQ¤ŒUB¸8ëÖ›&Eo>ÿ\0çIı›	ÇÌÿ\0˜ÿ\0\n@Q9õOsKå’İ«{û6ãæüéË¦[‚9ÌRöƒHw…ÿ\0ÂK¥úxOçEmxjÂüA§2–È:Ÿz+H;£*ŠÌÿÙ',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
