//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoPromotion
//{
//    class 임시보관01
//    {

// 네이버
        
//                                        Scroll_End = 0;
//                                        RowH = 0;
//                                        IList<IWebElement> he02 = helem1.FindElements(By.TagName("li"));
//                                        // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
//                                        foreach (IWebElement helem2 in he02)
//                                        {
//                                            RowH += 1;
//                                            TTL_RowH = ((xyi - 1) * 100) + RowH;

//                                            if (SelectNum == 0)       // if (xyj == 1)
//                                            {
//                                                _Delay_Pause(20);
//                                                if (Process_Stop == true) { goto End_Loop; }

//                                                txt_CheckingRank.Text = RowH.ToString();

//                                                int uxx = helem2.Location.X;
//                                                WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
//                                                if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
//                                            }
//                                            else
//                                            {
//                                                if (SelectNum <= RowH)
//                                                {
//                                                    if (Process_Stop == true) { goto End_Loop; }

//                                                    txt_CheckingRank.Text = RowH.ToString();

//                                                    int uxx = helem2.Location.X;
//                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
//                                                    if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
//                                                }
//                                            }

//                                            if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
//                                            {
//                                                //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
//                                            }
//                                            else if (SelectNum >= RowH)
//                                            {
//                                                //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
//                                            }
//                                            else if (Convert.ToInt32(txt_WorkNum.Text) >= TTL_RowH)
//                                            {
//                                                //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
//                                            }
//                                            else
//                                            {
//                                                if (RowH == 100)
//                                                {
//                                                    Page_End = true;
//                                                }

//                                                if (helem2.GetAttribute("innerText") != null)
//                                                {
//                                                    _Prod_Detail = false;

//                                                    #region  // 판매자정보 일부가져오기 -----------------------------------------------
//                                                    bool sInfoPick = false;
//                                                    IList<IWebElement> he03 = helem2.FindElements(By.TagName("span"));
//                                                    _Delay_Pause(100);
//                                                    foreach (IWebElement helem3 in he03)
//                                                    {
//                                                        if (helem3.GetAttribute("data-tiara-ordnum") != null)
//                                                        {
//                                                            if (helem3.GetAttribute("data-tiara-ordnum").IndexOf((RowH).ToString()) > -1)
//                                                            {
//                                                                if (sInfoPick == false)
//                                                                {
//                                                                    //int uxx = helem3.Location.X;
//                                                                    //WebDoc_Window_ScrollTo(chromeDriver, uxx, helem3.Location.Y + Convert.ToInt32(txt_Scroll.Text) - 300);
//                                                                    //if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }

//                                                                    db_ProductID = helem3.GetAttribute("data-tiara-id");
//                                                                    LastID = db_ProductID;

//                                                                    IList<IWebElement> he04 = helem3.FindElements(By.TagName("a"));
//                                                                    foreach (IWebElement helem4 in he04)
//                                                                    {
//                                                                        if (helem4.GetAttribute("href") != null)
//                                                                        {
//                                                                            db_ProductLink = helem4.GetAttribute("href");
//                                                                            break;
//                                                                        }
//                                                                    }
//                                                                    db_ProductImage = "";
//                                                                    db_ProductImage = helem3.GetAttribute("data-tiara-image");
//                                                                    db_ProductCate = helem3.GetAttribute("data-tiara-category");

//                                                                    sInfoPick = true;
//                                                                }
//                                                            }
//                                                        }
//                                                        if (helem3.GetAttribute("class").IndexOf("store_name") > -1)
//                                                        {
//                                                            db_StoreName = helem3.GetAttribute("innerText");
//                                                            db_StoreName = db_StoreName.Replace("'", "");
//                                                        }
//                                                        if (helem3.GetAttribute("class").IndexOf("product_name") > -1)
//                                                        {
//                                                            db_ProductName = helem3.GetAttribute("innerText");
//                                                            db_ProductName = db_ProductName.Replace("'", "");
//                                                        }
//                                                    }
//                                                    #endregion

//                                                    _Delay_Pause(10);
//                                                    if (Process_Stop == true) { goto End_Loop; }

//                                                    if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
//                                                    {
//                                                        Console.WriteLine(RowH + "r 시작 순번 이하");
//                                                    }
//                                                    else if (helem2.GetAttribute("innerText").IndexOf("연령 확인") > -1)
//                                                    {
//                                                        Console.WriteLine(RowH + "r 성인인증 상품 작업제외");
//                                                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  성인인증 상품 작업제외 : " + db_SellerName + "\n" + richTextBox1.Text;
//                                                    }
//                                                    else
//                                                    {
//                                                        SelectNum = RowH;

//                                                        #region  // 상품검색 시작 ====================================================
//                                                        IList<IWebElement> he05 = helem2.FindElements(By.TagName("a"));
//                                                        foreach (IWebElement helem5 in he05)
//                                                        {
//                                                            if (helem5.GetAttribute("href") != null)
//                                                            {
//                                                                helem5.Click();

//                                                                if (Convert.ToInt32(txt_WorkNum.Text) < TTL_RowH)
//                                                                {
//                                                                    txt_WorkNum.Text = TTL_RowH.ToString();
//                                                                }
//                                                                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                                                break;
//                                                            }
//                                                        }
//                                                        _Prod_Detail = true;
//                                                        #endregion

//                                                        #region  // 상세페이지에서 판매자정보 가져와 기존 작업내역이 있는지 비교하여 제외여부 확인 =================================
//                                                        Delay(2000);
//                                                        wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
//                                                        _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='txt_store']")));

//                                                        // 스토어정보 창 열기
//                                                        var queryButton = chromeDriver.FindElement(By.CssSelector("span[class='txt_store']"));
//                                                        queryButton.Click();
//                                                        Delay(3000);

//                                                        _Delay_Pause(10);
//                                                        if (Process_Stop == true) { goto End_Loop; }

//                                                        // 작업정보 가져오기 -------------------------------------
//                                                        Source = chromeDriver.PageSource;
//                                                        Source = Source.Replace("&quot;", @"""");
//                                                        Source = Source.Replace("&amp;", @"&");
//                                                        //string T_Source = SSplit(Source, @"""loginUser"":", 1);

//                                                        db_SellerName = "";
//                                                        db_SellerName = SSplit(SSplit(SSplit(Source, @"<dt>상호명</dt>", 1), @"<dd>", 1), @"</dd>", 0);
//                                                        db_RepresentativeName = SSplit(SSplit(SSplit(Source, @"<dt>대표자</dt>", 1), @"<dd>", 1), @"</dd>", 0);
//                                                        db_CallNumber = SSplit(SSplit(SSplit(Source, @"<dt>대표전화</dt>", 1), @"<dd>", 1), @"</dd>", 0);
//                                                        db_Address = SSplit(SSplit(SSplit(Source, @"<dt>사업장 소재지</dt>", 1), @"<dd>", 1), @"</dd>", 0);
//                                                        db_Email = SSplit(SSplit(SSplit(Source, @"<dt>대표메일</dt>", 1), @"<dd>", 1), @"</dd>", 0);
//                                                        // 작업정보 가져오기 -------------------------------------

//                                                        Recent_Store_YN = false;
//                                                        // 스토어명을 기존 작업 리스트와 비교
//                                                        for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
//                                                        {
//                                                            if (Recent_StoreList[xxj] == db_SellerName)
//                                                            {
//                                                                Console.WriteLine(RowH + "r  7일내 홍보됨 : " + db_SellerName);
//                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + richTextBox1.Text;
//                                                                Recent_Store_YN = true;

//                                                                break;
//                                                            }
//                                                        }
//                                                        chromeDriver.Navigate().Back();     // 판매자정보 화면 뒤로 가기
//                                                        Delay(1500);
//                                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                                        #endregion

//                                                        #region  // // 7일 이내 해당스토어 작업 없음 -----------------------------------------------
//                                                        if (Recent_Store_YN == false)
//                                                        {
//                                                            _Delay_Pause(10);
//                                                            if (Process_Stop == true) { goto End_Loop; }

//                                                            if (chromeDriver.PageSource.IndexOf("문의") > -1)
//                                                            {
//                                                                db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
//                                                                IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("span"));
//                                                                Delay(100);
//                                                                foreach (IWebElement helem11 in he11)
//                                                                {
//                                                                    if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
//                                                                    {
//                                                                        _Prod_Detail = true;
//                                                                        int uxx1 = helem11.Location.X;
//                                                                        WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
//                                                                        wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                                                                        Delay(500);

//                                                                        helem11.Click();

//                                                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                                                        wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                                                                        Delay(2000);

//                                                                        if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
//                                                                        {
//                                                                            // 문의하기
//                                                                            try
//                                                                            {
//                                                                                queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write ng-star-inserted']"));
//                                                                            }
//                                                                            catch
//                                                                            {
//                                                                                queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write']"));
//                                                                            }
//                                                                            queryButton.Click();
//                                                                            Delay(3000);

//                                                                            // 문의하기 팝업창 제어 ==============================================
//                                                                            string parentHandle = chromeDriver.CurrentWindowHandle;                                 // 현재 창의 핸들을 저장합니다.
//                                                                            IEnumerable<string> allHandles = chromeDriver.WindowHandles;                            // 모든 창의 핸들을 가져옵니다.
//                                                                            string dialogHandle = allHandles.FirstOrDefault(handle => handle != parentHandle);      // 부모 창을 제외한 다른 창을 찾습니다.
//                                                                            if (dialogHandle != null)
//                                                                            {
//                                                                                chromeDriver.SwitchTo().Window(dialogHandle);                                       // HTML Dialog 창으로 이동합니다.


//                                                                                IWebElement confirmButton = chromeDriver.FindElement(By.ClassName("fold_opt"));      // 문의유형 클릭
//                                                                                confirmButton.Click();
//                                                                                Delay(200);

//                                                                                var option = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/form/div[1]/select/option[7]"));
//                                                                                option.Click();

//                                                                                confirmButton.Click();
//                                                                                Delay(500);
//                                                                                confirmButton = chromeDriver.FindElement(By.Id("inpReview1"));      // 비밀글 클릭
//                                                                                confirmButton.Click();
//                                                                                Delay(200);

//                                                                                confirmButton = chromeDriver.FindElement(By.TagName("textarea[title='문의내용']"));     // 홍보 내역 입력
//                                                                                confirmButton.Click();
//                                                                                Delay(500);
//                                                                                confirmButton.SendKeys(txt_Promotion_Post.Text);
//                                                                                Delay(1000);



//                                                                                //// 등록 버튼 클릭
//                                                                                //confirmButton = chromeDriver.FindElement(By.Id("saveBtn"));
//                                                                                //confirmButton.Click();
//                                                                                //_Delay_Pause(500);

//                                                                                // 글 작성시에는 닫기 기능 미사용할 수 있음
//                                                                                chromeDriver.SwitchTo().Window(dialogHandle).Close();


//                                                                                chromeDriver.SwitchTo().Window(parentHandle);                                       // 부모 창으로 이동합니다.
//                                                                            }
//                                                                            // 문의하기 팝업창 제어 ==============================================  

//                                                                            _Delay_Pause(10);
//                                                                            if (Process_Stop == true) { goto End_Loop; }

//                                                                            // 작업 스토어 정보 변수에 저장
//                                                                            Recent_StoreList[Recent_Store_dno] = db_SellerName;
//                                                                            Recent_Store_dno += 1;
//                                                                            // 작업내용 저장 -----------------------------------------
//                                                                            try { ADONet_Conn.Close(); }
//                                                                            catch { }
//                                                                            try
//                                                                            {
//                                                                                string inSQL = "INSERT PROMOTION_WorkData ";
//                                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
//                                                                                inSQL += " ) VALUES ( ";
//                                                                                inSQL += " '" + p_UserID + "', ";
//                                                                                inSQL += " '" + db_Market + "', ";
//                                                                                inSQL += " '" + db_SKeyword + "', ";
//                                                                                inSQL += "  " + db_Rank + " , ";
//                                                                                inSQL += " '" + db_StoreName + "', ";
//                                                                                inSQL += " '" + db_ProductID + "', ";
//                                                                                inSQL += " '" + db_ProductLink + "', ";
//                                                                                inSQL += " '" + db_ProductCate + "', ";
//                                                                                inSQL += " '" + db_ProductName + "', ";
//                                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
//                                                                                inSQL += " '" + db_ProductImage + "', ";
//                                                                                inSQL += " '" + db_SellerName + "', ";
//                                                                                inSQL += " '" + db_CallNumber + "', ";
//                                                                                inSQL += " '" + db_Address + "', ";
//                                                                                inSQL += " '" + db_RepresentativeName + "', ";
//                                                                                inSQL += " '" + db_Email + "'  ";
//                                                                                inSQL += "  ) ";
//                                                                                ADONet_Conn.Open();
//                                                                                SqlCommand cmd0_1 = new SqlCommand();
//                                                                                cmd0_1.Connection = ADONet_Conn;
//                                                                                cmd0_1.CommandText = inSQL;
//                                                                                cmd0_1.ExecuteNonQuery();
//                                                                                ADONet_Conn.Close();

//                                                                                txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
//                                                                            }
//                                                                            catch
//                                                                            {
//                                                                                try { ADONet_Conn.Close(); }
//                                                                                catch { }
//                                                                                string inSQL = "INSERT PROMOTION_WorkData ";
//                                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
//                                                                                inSQL += " ) VALUES ( ";
//                                                                                inSQL += " '" + p_UserID + "', ";
//                                                                                inSQL += " '" + db_Market + "', ";
//                                                                                inSQL += " '" + db_SKeyword + "', ";
//                                                                                inSQL += "  " + db_Rank + " , ";
//                                                                                inSQL += " '" + db_StoreName + "', ";
//                                                                                inSQL += " '" + db_ProductID + "', ";
//                                                                                inSQL += " '" + db_ProductLink + "', ";
//                                                                                inSQL += " '" + db_ProductCate + "', ";
//                                                                                inSQL += " '" + db_ProductName + "', ";
//                                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
//                                                                                inSQL += " '" + db_ProductImage + "', ";
//                                                                                inSQL += " '" + db_SellerName + "', ";
//                                                                                inSQL += " '" + db_CallNumber + "', ";
//                                                                                inSQL += " '" + db_Address + "', ";
//                                                                                inSQL += " '" + db_RepresentativeName + "', ";
//                                                                                inSQL += " '" + db_Email + "'  ";
//                                                                                inSQL += "  ) ";
//                                                                                ADONet_Conn.Open();
//                                                                                SqlCommand cmd0_1 = new SqlCommand();
//                                                                                cmd0_1.Connection = ADONet_Conn;
//                                                                                cmd0_1.CommandText = inSQL;
//                                                                                cmd0_1.ExecuteNonQuery();
//                                                                                ADONet_Conn.Close();
//                                                                            }

//                                                                            try
//                                                                            {
//                                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
//                                                                            }
//                                                                            catch
//                                                                            {
//                                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
//                                                                            }

//                                                                            break;      // 상세페이지 루프 종료
//                                                                        }
//                                                                    }
//                                                                }

//                                                            }
//                                                            else
//                                                            {       // 문의하기 없어 빠져나옴

//                                                                _Prod_Detail = true;
//                                                            }
//                                                        }
//                                                        else
//                                                        {
//                                                            // 작업내역이 있어 빠져나옴
//                                                            Console.WriteLine("작업내역이 있어 빠져나옴");
//                                                        }
//                                                        #endregion

//                                                        if (RowH % 10 == 0)
//                                                        {
//                                                            try
//                                                            {
//                                                                string upSQL = "UPDATE PROMOTION_UserRecord   ";
//                                                                upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH + "'  ";
//                                                                upSQL += " WHERE UserID = '" + p_UserID + "' ";
//                                                                ADONet_Conn.Open();
//                                                                SqlCommand cmd0_1 = new SqlCommand();
//                                                                cmd0_1.Connection = ADONet_Conn;
//                                                                cmd0_1.CommandText = upSQL;
//                                                                cmd0_1.ExecuteNonQuery();
//                                                                ADONet_Conn.Close();

//                                                                txt_UserLastAct.Text = clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH;
//                                                            }
//                                                            catch { }
//                                                        }

//                                                        // 상세페이지에서 빠져나오기
//                                                        chromeDriver.Navigate().Back();
//                                                        Delay(500);
//                                                        //chromeDriver.Navigate().Refresh();
//                                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                                        Delay(500);

//                                                    }
//                                                }
//                                            }
//                                            if (_Prod_Detail == true)
//                                            {
//                                                break;  // ul 빠져나가기
//                                            }
//                                        }
//                                        Delay(1000);
//                                        if (_Prod_Detail == true)
//                                        {
//                                            break;  // ul 빠져나가기
//                                        }
//    }
//}
