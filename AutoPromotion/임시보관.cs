//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoPromotion
//{
//    class 임시보관
//    {



//        public void Selenium_기타(string xUrl)
//        {
//            this.Selenium_Paging(xUrl);

//            //// 저사양 페이지 로딩시간 연장 필요
//            //if (P_Width < 1500)
//            //{
//            //    _Delay_Pause(3000);
//            //}
//            //string xState = OnLoadWait(chromeDriver, xUrl, 1, "production-selling-question");

//            WebDriverWait wait = null;
//            if (Process_Stop == true) { goto End_Loop; }
//            if (chromeDriver.PageSource.IndexOf("회원가입") > -1)
//            {
//                chromeDriver.Navigate().GoToUrl("https://ohou.se/users/sign_in");
//                wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                Delay(1000);
//                _Delay_Pause(10);
//                if (Process_Stop == true) { goto End_Loop; }

//                //var queryButton = chromeDriver.FindElement(By.XPath("//*[@href='/users/auth/kakao']"));
//                //queryButton.Click();
//                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                //Delay(5000);
//                //_Delay_Pause(10);
//                //if (Process_Stop == true) { goto End_Loop; }

//                //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                //chromeDriver.Navigate().GoToUrl(xUrl);
//                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                //Delay(2000);
//                _Delay_Pause(10);
//                if (Process_Stop == true) { goto End_Loop; }

//                AutoClosingMessageBox.Show("오늘의집 회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 2000);
//                return;
//            }
//            else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
//            {
//                Key_WorkState = true;
//                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
//                goto End_Loop;
//            }

//            if (chromeDriver.Url.IndexOf("ohou.se") == -1)
//            {
//                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [오늘의집 로그인안됨]" + "\n" + richTextBox1.Text;
//                Source = "오늘의집 PASS";
//            }
//            else
//            {
//                int ColH = 1;
//                int RowH = 1;
//                int ScrollLoop = 0;

//                while (RowH <= Convert.ToInt32(txt_Rank_End.Text))
//                {
//                    _Delay_Pause(10);
//                    if (Process_Stop == true) { goto End_Loop; }

//                    //int xRowH = 1;
//                    //try
//                    //{
//                    //    var ProdList = chromeDriver.FindElement(By.CssSelector("[href$='?affect_id=" + RowH + "&affect_type=StoreSearchResult']"));
//                    //}
//                    //catch
//                    //{
//                    //    //((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
//                    //    while (xRowH <= RowH)
//                    //    {
//                    //        try
//                    //        {
//                    //            var ProdList = chromeDriver.FindElement(By.CssSelector("[href$='?affect_id=" + xRowH + "&affect_type=StoreSearchResult']"));
//                    //            int uxx = ProdList.Location.X;
//                    //            WebDoc_Window_ScrollTo(chromeDriver, uxx, ProdList.Location.Y + Convert.ToInt32(txt_Scroll.Text));
//                    //            if (xRowH % 3 == 0)
//                    //            {
//                    //                _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text) * 2);
//                    //            }
//                    //        }
//                    //        catch { }

//                    //        xRowH += 1;
//                    //    }
//                    //}

//                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
//                    IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[href$='?affect_id=" + RowH + "&affect_type=StoreSearchResult']")));
//                    IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("article"));
//                    foreach (IWebElement helem1 in he01)
//                    {
//                        bool _Prod_Detail = false;
//                        _Delay_Pause(10);
//                        if (Process_Stop == true) { goto End_Loop; }
//                        if (helem1.GetAttribute("class") == "production-item")
//                        {
//                            txt_CheckingRank.Text = RowH.ToString();

//                            IList<IWebElement> he02 = helem1.FindElements(By.TagName("a"));
//                            _Delay_Pause(100);
//                            foreach (IWebElement helem2 in he02)
//                            {
//                                _Delay_Pause(10);
//                                if (Process_Stop == true) { goto End_Loop; }
//                                if (helem2.GetAttribute("href") != null)
//                                {
//                                    if (helem2.GetAttribute("href").IndexOf("_id=" + RowH + "&") > -1)
//                                    {
//                                        int uxx = helem2.Location.X;
//                                        //WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y - 100);
//                                        WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
//                                        if (RowH % 3 == 0)
//                                        {
//                                            _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text));
//                                        }

//                                        //// 문의하기 처리된 스토어는 제외하는 루틴 ---------------------------------------
//                                        Recent_Store_YN = false;
//                                        string xStoreName = "";
//                                        if (RowH >= Convert.ToInt32(txt_Rank_Start.Text))
//                                        {
//                                            string Recent_ProdID = SSplit(SSplit(helem2.GetAttribute("href").ToString(), "productions/", 1), "/", 0);
//                                            IList<IWebElement> heDe_01 = helem1.FindElements(By.TagName("span"));
//                                            foreach (IWebElement helemDe1 in heDe_01)
//                                            {
//                                                if (helemDe1.GetAttribute("class") == "production-item__header__brand")
//                                                {
//                                                    // 요소에서 스토어명 가져오기
//                                                    xStoreName = helemDe1.GetAttribute("innerText");
//                                                    // 스토어명을 기존 작업 리스트와 비교
//                                                    for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
//                                                    {
//                                                        if (Recent_StoreList[xxj] == xStoreName)
//                                                        {
//                                                            Console.WriteLine(RowH + "r  7일 이내 작업 있음 : " + xStoreName);
//                                                            Recent_Store_YN = true;
//                                                            break;
//                                                        }
//                                                    }
//                                                }
//                                            }
//                                        }

//                                        if (RowH >= Convert.ToInt32(txt_Rank_Start.Text) && Recent_Store_YN == false)
//                                        {
//                                            _Delay_Pause(200);

//                                            //richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [오늘의집 " + RowH + "r  상품 클릭완료]" + "\n" + richTextBox1.Text;
//                                            helem2.Click();
//                                            Console.WriteLine("상품클릭 " + RowH);
//                                            txt_WorkNum.Text = RowH.ToString();
//                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());

//                                            //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                            _Delay_Pause(1000);
//                                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
//                                            wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));

//                                            //IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("a[id='production-selling-suggestion']"))); // 사용가능
//                                            try
//                                            {
//                                                IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("table[class='production-selling-table']")));
//                                                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
//                                                Console.WriteLine("엘리먼트확인 " + RowH);
//                                            }
//                                            catch
//                                            {
//                                                Console.WriteLine("상품선택후문의가능 " + RowH);
//                                                try
//                                                {
//                                                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
//                                                }
//                                                catch { }

//                                                //var Xpath = "//span[contains(@class,'production-select-text-button__text')][contains(text(),'상품을 선택하세요.')]]";
//                                                //var radio = chromeDriver.FindElement(By.XPath(Xpath));
//                                                //radio.Click();

//                                                //var ProdButton = chromeDriver.FindElement(By.CssSelector("button[text()='상품을 선택하세요.']"));
//                                                //uxx = ProdButton.Location.X;
//                                                //WebDoc_Window_ScrollTo(chromeDriver, uxx, ProdButton.Location.Y - 200);
//                                                //_Delay_Pause(100);
//                                                //ProdButton.Click();

//                                                //ProdButton = chromeDriver.FindElement(By.CssSelector("div[class='production-select-item__contents']"));
//                                                //ProdButton.Click();
//                                            }

//                                            _Delay_Pause(10);
//                                            if (Process_Stop == true) { goto End_Loop; }

//                                            if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
//                                            {
//                                                db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
//                                                IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("button"));
//                                                _Delay_Pause(100);
//                                                foreach (IWebElement helem11 in he11)
//                                                {
//                                                    if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
//                                                    {
//                                                        Console.WriteLine("문의하기확인 " + RowH);
//                                                        // 작업정보 가져오기 -------------------------------------
//                                                        Source = chromeDriver.PageSource;
//                                                        Source = Source.Replace("&quot;", @"""");
//                                                        Source = Source.Replace("&amp;", @"&");
//                                                        string T_Source = SSplit(Source, @"""loginUser"":", 1);
//                                                        db_StoreName = "";
//                                                        try
//                                                        {
//                                                            db_StoreName = SSplit(SSplit(SSplit(Source, @"<a class=""production-selling-header__title__brand""", 1), @">", 1), @"</a", 0);
//                                                        }
//                                                        catch
//                                                        {
//                                                        }
//                                                        db_SellerName = "";
//                                                        try
//                                                        {
//                                                            db_SellerName = SSplit(SSplit(SSplit(Source, @"<th>상호</th>", 1), @"<td>", 1), @"</td>", 0);
//                                                        }
//                                                        catch
//                                                        {
//                                                            db_SellerName = SSplit(SSplit(Source, @"<span class=""production-item__header__brand"">", 1), @"</span>", 0);
//                                                            db_SellerName = db_SellerName.Replace(" ", "");
//                                                        }
//                                                        string t_Cate = SSplit(SSplit(T_Source, @"<ol class=""commerce-", 1), @"</ol>", 0);
//                                                        db_ProductCate = "";

//                                                        for (int xl = 0; xl < UBound_int(t_Cate, @"<li"); xl++)
//                                                        {
//                                                            string t_CateBound = SSplit(t_Cate, @"<li", xl + 1);
//                                                            if (xl == UBound_int(t_Cate, @"<li") - 2)
//                                                            {
//                                                                db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0);
//                                                                db_ProductID = SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @"affect_id=", 1), @"""", 0);
//                                                            }
//                                                            else
//                                                            {
//                                                                db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0) + " > ";
//                                                            }
//                                                            if (xl == UBound_int(t_Cate, @"<li") - 2) { break; }
//                                                            //MessageBox.Show(SSplit(t_Cate, @"<li", xl));
//                                                        }
//                                                        db_ProductName = SSplit(SSplit(Source, @"""og:title"" content=""", 2), @"""", 0);
//                                                        db_ProductName = db_ProductName.Replace("'", "");
//                                                        db_ProductImage = SSplit(SSplit(Source, @"""og:image"" content=""", 2), @"?", 0);
//                                                        db_CallNumber = SSplit(SSplit(SSplit(Source, @"고객센터 전화번호</th>", 1), @"<td>", 1), @"</td", 0);
//                                                        db_Address = SSplit(SSplit(SSplit(Source, @"보내실 곳", 1), @"<td>", 1), @"</td>", 0);
//                                                        // 작업정보 가져오기 -------------------------------------
//                                                        Console.WriteLine("판매자정보확인 " + RowH);


//                                                        _Prod_Detail = true;
//                                                        int uxx1 = helem11.Location.X;
//                                                        WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
//                                                        wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                                                        _Delay_Pause(100);

//                                                        helem11.Click();
//                                                        //richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   → 오늘의집 " + RowH + "r  문의하기 클릭완료" + "\n" + richTextBox1.Text;
//                                                        Console.WriteLine("문의하기클릭 " + RowH);

//                                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
//                                                        wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
//                                                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 20000)");
//                                                        _Delay_Pause(2000);

//                                                        if (chromeDriver.PageSource.IndexOf("문의유형") > -1)
//                                                        {
//                                                            Console.WriteLine("문의유형확인 " + RowH);
//                                                            //try
//                                                            //{
//                                                            //    IAlert alert = chromeDriver.SwitchTo().Alert();
//                                                            //    alert.Accept();
//                                                            //}
//                                                            //catch (NoAlertPresentException)
//                                                            //{
//                                                            //    // 팝업창이 Alert 창이 아닌 경우에는 NoAlertPresentException 예외가 발생합니다.
//                                                            //    // 이 경우 아무런 처리를 하지 않습니다.
//                                                            //    MessageBox.Show("상세페이지 문의하기");
//                                                            //}




//                                                            // 모달창 클릭방법 으로 변경 필요 (오류 => var queryButton = chromeDriver.FindElement(By.PartialLinkText("기타"));)

//                                                            // 기타
//                                                            var queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[4]/div[6]"));
//                                                            queryButton.Click();
//                                                            _Delay_Pause(500);

//                                                            try
//                                                            {
//                                                                // 선택 안함
//                                                                queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/div[2]/label"));
//                                                                queryButton.Click();
//                                                                _Delay_Pause(500);
//                                                            }
//                                                            catch { }

//                                                            // 홍보 내역 입력
//                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
//                                                            queryButton.Click();
//                                                            Delay(500);
//                                                            queryButton.SendKeys(txt_Promotion_Post.Text);
//                                                            Delay(1000);

//                                                            // 비밀글로 문의하기
//                                                            try
//                                                            {
//                                                                queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[8]/label"));
//                                                            }
//                                                            catch
//                                                            {
//                                                                queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/label"));
//                                                            }
//                                                            queryButton.Click();
//                                                            _Delay_Pause(500);

//                                                            // 크게 늘어난 textarea 창을 알맞게 줄여 줌
//                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
//                                                            queryButton.Clear();
//                                                            _Delay_Pause(1000);


//                                                            //chromeDriver.FindElement(By.ClassName("body").SendKeys(Keys.PageDown));
//                                                            //((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 10000)");
//                                                            //Delay(500);

//                                                            if (PGM_Start_Manual == true)
//                                                            {
//                                                                //// 문의글 작성내용 지우기 영역인데, 구동이 안되고 있음
//                                                                //queryButton.SendKeys("^A");
//                                                                //queryButton.SendKeys("");
//                                                                //Delay(1000);

//                                                                //input.Clear();
//                                                                //input.Click();
//                                                                //input.SendKeys(Keys.Control + "A");
//                                                                //input.SendKeys("Hello~Bryan");
//                                                            }

//                                                            _Delay_Pause(10);
//                                                            if (Process_Stop == true) { goto End_Loop; }


//                                                            //// 완료 버튼 클릭
//                                                            //queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[10]/button"));
//                                                            //queryButton.Click();
//                                                            //_Delay_Pause(500);


//                                                            // 문의하기 완료 후 화면 체크
//                                                            try
//                                                            {
//                                                                chromeDriver.Navigate().Back();
//                                                                Console.WriteLine("상세에서리스트로이동 " + RowH);
//                                                                _Delay_Pause(3000);
//                                                                // 문의하기 완료 처리가 안되면 '나가기' 처리
//                                                                if (chromeDriver.PageSource.IndexOf("나가기") > -1 && chromeDriver.PageSource.IndexOf("취소") > -1)
//                                                                {
//                                                                    // 나가기 버튼 클릭
//                                                                    try
//                                                                    {
//                                                                        queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/button[2]"));
//                                                                        queryButton.Click();
//                                                                    }
//                                                                    catch { }

//                                                                    AutoClosingMessageBox.Show("문의하기 에서 빠져 나오는 중", "메세지박스 자동종료창", 2000);
//                                                                }
//                                                            }
//                                                            catch { }

//                                                            _Delay_Pause(100);
//                                                            chromeDriver.Navigate().Refresh();
//                                                            _Delay_Pause(1000);

//                                                            // 작업 스토어 정보 변수에 저장
//                                                            Recent_StoreList[Recent_Store_dno] = xStoreName;
//                                                            Recent_Store_dno += 1;
//                                                            // 작업내용 저장 -----------------------------------------
//                                                            //if (ADONet_Conn.State == ConnectionState.Open) { ADONet_Conn.Close(); }
//                                                            try
//                                                            {
//                                                                ADONet_Conn.Close();
//                                                            }
//                                                            catch { }
//                                                            try
//                                                            {
//                                                                string inSQL = "INSERT PROMOTION_WorkData ";
//                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address ";
//                                                                inSQL += " ) VALUES ( ";
//                                                                inSQL += " '" + p_UserID + "', ";
//                                                                inSQL += " '" + db_Market + "', ";
//                                                                inSQL += " '" + db_SKeyword + "', ";
//                                                                inSQL += "  " + db_Rank + " , ";
//                                                                inSQL += " '" + db_StoreName + "', ";
//                                                                inSQL += " '" + db_ProductID + "', ";
//                                                                inSQL += " 'https://ohou.se/productions/" + db_ProductID + "/selling', ";
//                                                                inSQL += " '" + db_ProductCate + "', ";
//                                                                inSQL += " '" + db_ProductName + "', ";
//                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
//                                                                inSQL += " '" + db_ProductImage + "', ";
//                                                                inSQL += " '" + db_SellerName + "', ";
//                                                                inSQL += " '" + db_CallNumber + "', ";
//                                                                inSQL += " '" + db_Address + "'  ";
//                                                                inSQL += "  ) ";
//                                                                ADONet_Conn.Open();
//                                                                SqlCommand cmd0_1 = new SqlCommand();
//                                                                cmd0_1.Connection = ADONet_Conn;
//                                                                cmd0_1.CommandText = inSQL;
//                                                                cmd0_1.ExecuteNonQuery();
//                                                                ADONet_Conn.Close();

//                                                                txt_Work_Count.Text = "총 " + (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
//                                                            }
//                                                            catch
//                                                            {
//                                                                //if (ADONet_Conn.State == ConnectionState.Open)
//                                                                //{ 
//                                                                //    ADONet_Conn.Close();
//                                                                //}
//                                                                try
//                                                                {
//                                                                    ADONet_Conn.Close();
//                                                                }
//                                                                catch { }
//                                                                string inSQL = "INSERT PROMOTION_WorkData ";
//                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address ";
//                                                                inSQL += " ) VALUES ( ";
//                                                                inSQL += " '" + p_UserID + "', ";
//                                                                inSQL += " '" + db_Market + "', ";
//                                                                inSQL += " '" + db_SKeyword + "', ";
//                                                                inSQL += "  " + db_Rank + " , ";
//                                                                inSQL += " '" + db_StoreName + "', ";
//                                                                inSQL += " '" + db_ProductID + "', ";
//                                                                inSQL += " 'https://ohou.se/productions/" + db_ProductID + "/selling', ";
//                                                                inSQL += " '" + db_ProductCate + "', ";
//                                                                inSQL += " '" + db_ProductName + "', ";
//                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
//                                                                inSQL += " '" + db_ProductImage + "', ";
//                                                                inSQL += " '" + db_SellerName + "', ";
//                                                                inSQL += " '" + db_CallNumber + "', ";
//                                                                inSQL += " '" + db_Address + "'  ";
//                                                                inSQL += "  ) ";
//                                                                ADONet_Conn.Open();
//                                                                SqlCommand cmd0_1 = new SqlCommand();
//                                                                cmd0_1.Connection = ADONet_Conn;
//                                                                cmd0_1.CommandText = inSQL;
//                                                                cmd0_1.ExecuteNonQuery();
//                                                                ADONet_Conn.Close();
//                                                            }
//                                                            Console.WriteLine("작업내용저장 " + RowH);

//                                                            try
//                                                            {
//                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + RowH + "r  문의완료 : " + db_StoreName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
//                                                            }
//                                                            catch
//                                                            {
//                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + RowH + "r  문의완료 : " + db_StoreName + "_" + db_ProductName + "\n" + richTextBox1.Text;
//                                                            }
//                                                            Console.WriteLine(db_StoreName + "_" + db_ProductName);

//                                                            break;      // 상세페이지 루프 종료




//                                                        }
//                                                    }
//                                                }






//                                                //if (chromeDriver.PageSource.IndexOf("_id=" + RowH + "&") > -1 && chromeDriver.PageSource.IndexOf("바로구매") > -1)
//                                                //{
//                                                //    Console.WriteLine("상세페이지 로딩완료");


//                                                //}
//                                            }
//                                            else
//                                            {       // 문의하기 없어 빠져나옴

//                                                _Prod_Detail = true;
//                                                //_Delay_Pause(1000);
//                                                chromeDriver.Navigate().Back();

//                                                _Delay_Pause(100);
//                                                chromeDriver.Navigate().Refresh();
//                                                _Delay_Pause(1000);
//                                            }
//                                        }
//                                        //Console.WriteLine("상세작업완료 " + RowH);
//                                        RowH += 1;
//                                        break;
//                                    }
//                                }
//                            }
//                        }
//                        if (_Prod_Detail == true)
//                        {
//                            if (RowH > Convert.ToInt32(txt_Rank_Start.Text))
//                            {
//                                ScrollLoop += 1;
//                                if (ScrollLoop >= 6)
//                                {
//                                    _Delay_Pause(100);
//                                    //chromeDriver.Navigate().Refresh();
//                                    //_Delay_Pause(1000);
//                                }
//                                else
//                                {
//                                    _Delay_Pause(100);
//                                }
//                            }
//                            break;
//                        }
//                    }
//                    if (RowH >= Convert.ToInt32(txt_Rank_End.Text) || ColH >= 5)
//                    {
//                        Key_WorkState = true;
//                        ColH = 1;
//                    }
//                    else
//                    {
//                        ColH += 1;
//                    }
//                }
//                chromeDriver.Navigate().Back();
//                Console.WriteLine("구글페이지로이동 " + RowH);
//            }

//        End_Loop:
//            Console.WriteLine("작업을 종료합니다.");
//        }

//    }
//}
