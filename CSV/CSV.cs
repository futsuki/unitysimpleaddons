using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// wroted by http://dobon.net/vb/dotnet/file/readcsvfile.html
public static class CSV
{
	public class CSVException : Exception
	{
		public CSVException()
		{
		}
		
		public CSVException(string message)
			: base(message)
		{
		}
		
		public CSVException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
	
	public static string ToCSV(this List<List<string>> csv)
	{
		var quoteRE = new System.Text.RegularExpressions.Regex("^[ \t\n]|[\n\"]|[ \t\n]$");
		var sb = new StringBuilder();
		foreach (var line in csv) {
			bool first = true;
			foreach (var elem in line) {
				var m = quoteRE.Match(elem);
				if (first) {
					first = false;
				}
				else {
					sb.Append(",");
				}
				if (m.Success) {
					sb.Append("\"");
					sb.Append(elem.Replace("\"", "\"\""));
					sb.Append("\"");
				}
				else {
					sb.Append(elem.Replace("\"", "\"\""));
				}
			}	
			sb.Append("\n");
		}
		sb.Length = System.Math.Max(sb.Length - 1, 0);
		return sb.ToString();
	}

	public static List<List<string>> ParseCSV(this string csvText)
	{
		//前後の改行を削除しておく
		csvText = csvText.Trim(new char[] { '\r', '\n' });
		
		var csvRecords = new List<List<string>>();
		var csvFields = new List<string>();
		
		int csvTextLength = csvText.Length;
		int startPos = 0, endPos = 0;
		string field = "";
		
		while (true)
		{
			//空白を飛ばす
			while (startPos < csvTextLength &&
			       (csvText[startPos] == ' ' || csvText[startPos] == '\t'))
			{
				startPos++;
			}
			
			//データの最後の位置を取得
			if (startPos < csvTextLength && csvText[startPos] == '"')
			{
				//"で囲まれているとき
				//最後の"を探す
				endPos = startPos;
				while (true)
				{
					endPos = csvText.IndexOf('"', endPos + 1);
					if (endPos < 0)
					{
						throw new CSVException("\"が不正");
					}
					//"が2つ続かない時は終了
					if (endPos + 1 == csvTextLength || csvText[endPos + 1] != '"')
					{
						break;
					}
					//"が2つ続く
					endPos++;
				}
				
				//一つのフィールドを取り出す
				field = csvText.Substring(startPos, endPos - startPos + 1);
				//""を"にする
				field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");
				
				endPos++;
				//空白を飛ばす
				while (endPos < csvTextLength &&
				       csvText[endPos] != ',' && csvText[endPos] != '\n')
				{
					endPos++;
				}
			}
			else
			{
				//"で囲まれていない
				//カンマか改行の位置
				endPos = startPos;
				while (endPos < csvTextLength &&
				       csvText[endPos] != ',' && csvText[endPos] != '\n')
				{
					endPos++;
				}
				
				//一つのフィールドを取り出す
				field = csvText.Substring(startPos, endPos - startPos);
				//後の空白を削除
				field = field.TrimEnd();
			}
			
			//フィールドの追加
			csvFields.Add(field);
			
			//行の終了か調べる
			if (endPos >= csvTextLength || csvText[endPos] == '\n')
			{
				//行の終了
				//レコードの追加
				csvRecords.Add(csvFields.ToList());
				csvFields.Clear();
				//csvFields = new List<string>(csvFields.Count);
				
				if (endPos >= csvTextLength)
				{
					//終了
					break;
				}
			}
			
			//次のデータの開始位置
			startPos = endPos + 1;
		}
		
		return csvRecords.ToList();
	}
}
