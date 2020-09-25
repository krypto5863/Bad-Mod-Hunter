using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
	class Lib
	{
		public static HashSet<string> slots = new HashSet<string>()
		{
			"body",
			"ik",
			"head",
			"eye",
			"hairf",
			"hairr",
			"hairs",
			"hairt",
			"wear",
			"skirt",
			"onepiece",
			"mizugi",
			"panz",
			"bra",
			"stkg",
			"shoes",
			"headset",
			"glove",
			"acchead",
			"hairaho",
			"acchana",
			"accha",
			"acckami_1_",
			"accmimir",
			"acckamisubr",
			"accnipr",
			"handitemr",
			"_ik_handr",
			"acckubi",
			"bip01 spine1a",
			"acckubiwa",
			"bip01 neck",
			"accheso",
			"accude",
			"accashi",
			"accsenaka",
			"accshippo",
			"bip01 spine",
			"accanl",
			"accvag",
			"kubiwa",
			"megane",
			"accxxx",
			"chinko",
			"bip01 pelvis",
			"chikubi",
			"acchat",
			"kousoku_upper",
			"kousoku_lower",
			"seieki_naka",
			"seieki_hara",
			"seieki_face",
			"seieki_mune",
			"seieki_hip",
			"seieki_ude",
			"seieki_ashi",
			"accnipl",
			"accmimil",
			"acckamisubl",
			"acckami_2_",
			"acckami_3_",
			"handiteml",
			"_ik_handl",
			"underhair",
			"moza",
			"end"
		};

		public static HashSet<string> category = new HashSet<string>()

		{
			"null_mpn",
			"munel",
			"munes",
			"munetare",
			"regfat",
			"arml",
			"hara",
			"regmeet",
			"kubiscl",
			"udescl",
			"eyescl",
			"eyesclx",
			"eyescly",
			"eyeposx",
			"eyeposy",
			"eyeclose",
			"eyeballposx",
			"eyeballposy",
			"eyeballsclx",
			"eyeballscly",
			"earnone",
			"earelf",
			"earrot",
			"earscl",
			"nosepos",
			"nosescl",
			"faceshape",
			"faceshapeslim",
			"mayushapein",
			"mayushapeout",
			"mayux",
			"mayuy",
			"mayurot",
			"headx",
			"heady",
			"douper",
			"sintyou",
			"koshi",
			"kata",
			"west",
			"muneupdown",
			"muneyori",
			"muneyawaraka",
			"mayuthick",
			"mayulong",
			"yorime",
			"mabutaupin",
			"mabutaupin2",
			"mabutaupmiddle",
			"mabutaupout",
			"mabutaupout2",
			"mabutalowin",
			"mabutalowupmiddle",
			"mabutalowupout",
			"body",
			"moza",
			"head",
			"hairf",
			"hairr",
			"hairt",
			"hairs",
			"hairaho",
			"haircolor",
			"skin",
			"acctatoo",
			"accnail",
			"underhair",
			"hokuro",
			"acctatoo",
			"mayu",
			"lip",
			"eye",
			"eye_hi",
			"eye_hi_r",
			"chikubi",
			"chikubicolor",
			"eyewhite",
			"nose",
			"facegloss",
			"matsuge_up",
			"matsuge_low",
			"futae",
			"wear",
			"skirt",
			"mizugi",
			"bra",
			"panz",
			"stkg",
			"shoes",
			"headset",
			"glove",
			"acchead",
			"accha",
			"acchana",
			"acckamisub",
			"acckami",
			"accmimi",
			"accnip",
			"acckubi",
			"acckubiwa",
			"accheso",
			"accude",
			"accashi",
			"accsenaka",
			"accshippo",
			"accanl",
			"accvag",
			"megane",
			"accxxx",
			"handitem",
			"acchat",
			"onepiece",
			"set_maidwear",
			"set_mywear",
			"set_underwear",
			"set_body",
			"folder_eye",
			"folder_mayu",
			"folder_underhair",
			"folder_skin",
			"folder_eyewhite",
			"folder_matsuge_up",
			"folder_matsuge_low",
			"folder_futae",
			"kousoku_upper",
			"kousoku_lower",
			"seieki_naka",
			"seieki_hara",
			"seieki_face",
			"seieki_mune",
			"seieki_hip",
			"seieki_ude",
			"seieki_ashi"
		};




		public static HashSet<string> delItem = new HashSet<string>()
		{
			"_i_hairt_del.menu",
			"_i_hairs_del.menu",
			"_i_acctatoo_del.menu",
			"_i_accnail_del.menu",
			"_i_underhair_del.menu",
			"_i_hokuro_del.menu",
			"_i_lip_del.menu",
			"_i_wear_del.menu",
			"_i_skirt_del.menu",
			"_i_mizugi_del.menu",
			"_i_bra_del.menu",
			"_i_panz_del.menu",
			"_i_stkg_del.menu",
			"_i_shoes_del.menu",
			"_i_headset_del.menu",
			"_i_glove_del.menu",
			"_i_acchead_del.menu",
			"_i_hairaho_del.menu",
			"_i_accha_del.menu",
			"_i_acchana_del.menu",
			"_i_acckamisub_del.menu",
			"_i_acckami_del.menu",
			"_i_accmimi_del.menu",
			"_i_accnip_del.menu",
			"_i_acckubi_del.menu",
			"_i_acckubiwa_del.menu",
			"_i_accheso_del.menu",
			"_i_accude_del.menu",
			"_i_accashi_del.menu",
			"_i_accsenaka_del.menu",
			"_i_accshippo_del.menu",
			"_i_accanl_del.menu",
			"_i_accvag_del.menu",
			"_i_megane_del.menu",
			"_i_accxxx_del.menu",
			"_i_handitem_del.menu",
			"_i_acchat_del.menu",
			"_i_onepiece_del.menu",
			"_i_underhair_folder_del.menu",
			"_i_kousokuu_del.menu",
			"_i_kousokul_del.menu"
		};


		public static HashSet<string> cSetType = new HashSet<string>()
		{
			"eye",
			"haircolor",
			"skin",
			"mayu",
			"underhair",
			"chikubicolor",
			"matsuge_up",
			"matsuge_low",
		};


		public static HashSet<string> recoSlot = new HashSet<string>()
		{
			"eye_l",
			"eye_r",
			"hair",
			"eye_brow",
			"under_hair",
			"skin",
			"nipple",
			"hair_outline",
			"skin_outline"
		};

		public static HashSet<string> attachPoints = new HashSet<string>() {
			"顔",
			"目",
			"目ハイライト",
			"眉",
			"ほくろ",
			"唇",
			"歯",
			"前髪",
			"後髪",
			"横髪",
			"エクステ髪",
			"アホ毛",
			"肌",
			"タトゥ\u30FC",
			"帽子",
			"ヘッドドレス",
			"トップス",
			"ボトムス",
			"ワンピ\u30FCス",
			"水着",
			"靴下",
			"靴",
			"前髪アクセ",
			"メガネ",
			"アイマスク",
			"鼻アクセ",
			"耳アクセ",
			"手袋",
			"ネックレス",
			"リボン",
			"腕アクセ",
			"へそアクセ",
			"足首アクセ",
			"背中アクセ",
			"尻尾",
			"チョ\u30FCカ\u30FC",
			"ヘアカラ\u30FC",
			"私服セット",
			"メイド服セット",
			"ネイル",
			"なし",
			"アナル",
			"ヴァギナ",
			"お下げ１",
			"お下げ２",
			"クリトリス",
			"ふたなり",
			"へそ",
			"もみあげ右",
			"もみあげ左",
			"乳首右",
			"子宮口",
			"尿道",
			"直腸",
			"眼鏡",
			"耳たぶ右",
			"耳たぶ左",
			"膀胱",
			"舌",
			"舌先端",
			"額",
			"髪飾り",
			"鼻",
			"乳首左"
		};

		public static HashSet<String> tSlot = new HashSet<string>()
		{
			"_maintex",
			"_shadowtex",
			"_toonramp",
			"_hitex",
			"_shadowcolor",
			"_outlinecolor",
			"_rimcolor",
			"_color",
			"_outlinetoonramp",
			"_outlinetex",
			"_shadowratetoon"
		};

		public static HashSet<String> hBranch = new HashSet<string>()
		{
			"消去node",
			"属性追加",
			"メニューフォルダ",
			"unsetitem",
			"name",
			"commenttype",
			"paramset",
			"param2",
			"node表示",
			"node消去",
			"blendset",
			"bonemorph",
			"mouthonly"
		};

		public static HashSet<String> mCat = new HashSet<string>()
		{
			"dress",
			"head",
			"body",
			"man"
		};

		public static HashSet<String> cState = new HashSet<string>()
		{
			"めくれスカート",
			"めくれスカート後ろ",
			"パンツずらし"
		};

		public static HashSet<String> comment = new HashSet<string>()
		{
			"onclickmenu",
			"消去node設定終了",
			"消去node設定開始"
		};

		public static bool IsCat(string input)
		{
			if (Lib.category.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsDelItem(string input)
		{
			if (Lib.delItem.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsRecoSlot(string input)
		{
			if (Lib.recoSlot.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsCSetType(string input)
		{
			if (Lib.cSetType.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsSlot(string input)
		{
			if (Lib.slots.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsTexSlot(string input)
		{
			if (Lib.tSlot.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsHandled(string input)
		{
			if (hBranch.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsMCat(string input)
		{
			if (mCat.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}
		public static bool IsClothState(string input)
		{

			if (cState.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsComment(string input)
		{

			if (comment.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}

		public static bool IsAttachPoint(string input)
		{
			if (Lib.attachPoints.Contains(input.ToLower()))
			{
				return true;
			}
			return false;
		}
	}
}
