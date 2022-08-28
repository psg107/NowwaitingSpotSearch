using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowwaitingSpotSearch.Models;
using NowwaitingSpotSearch.Contexts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NowwaitingSpotSearch.Apis
{
    [Controller]
    [Route("oauth")]
    public class AuthController : Controller
    {
        private readonly HttpClient client;
        private readonly WaitingDBContext context;

        public AuthController(HttpClient client, WaitingDBContext context)
        {
            this.client = client;
            this.context = context;
        }

        [Route("grant")]
        public async Task<ContentResult> Grant(string code, string state)
        {
            try
            {
                //동일 state를 사용하는 데이터가 존재하면 반환
                var authToken = context.AuthTokens.Where(x => x.State == state).FirstOrDefault();
                if (authToken != null)
                {
                    throw new Exception("이미 서버에 토큰이 존재합니다");
                }

                //인증
                var response = await client.PostAsync($"https://slack.com/api/oauth.access?client_id=3507541088166.3969218284066&client_secret=82362048cdbc7e26ae3bcd201b1110b7&code={code}", null);
                var content = await response.Content.ReadAsStringAsync();
                var oauthResponse = JsonSerializer.Deserialize<OAuthResponse>(content);
                if (string.IsNullOrEmpty(oauthResponse.AccessToken))
                {
                    throw new Exception($"fail1 {content}");
                }

                //accesstoken이 존재하면 state 업데이트 후 반환
                var savedAuthToken = context.AuthTokens.Where(x => x.AccessToken == oauthResponse.AccessToken).FirstOrDefault();
                if (savedAuthToken != null)
                {
                    savedAuthToken.State = state;
                    await context.SaveChangesAsync();

                    return Content($"<h1>토큰 정보가 변경되었습니다. 브라우저를 종료 후 SlackProfile.exe를 다시 실행하세요.</h1>".Trim(), "text/html; charset=utf-8");
                }

                //신규 토큰
                context.AuthTokens.Add(new Entities.AuthTokenEntity
                {
                    AccessToken = oauthResponse.AccessToken,
                    Scope = oauthResponse.Scope,
                    TeamName = oauthResponse.TeamName,
                    TeamId = oauthResponse.TeamId,
                    EnterpriseId = oauthResponse.EnterpriseId,
                    State = state
                });
                var saved = await context.SaveChangesAsync();
                if (saved == 0)
                {
                    throw new Exception("fail2");
                }

                return Content($"<h1>토큰이 생성되었습니다. 브라우저를 종료 후 SlackProfile.exe를 다시 실행하세요.</h1>".Trim(), "text/html; charset=utf-8");
            }
            catch (Exception ex)
            {
                return Content($"<h1>로그인 중 오류가 발생했습니다.</h1>".Trim(), "text/html; charset=utf-8");
            }
        }

        [Route("token")]
        public string GetToken(string state)
        {
            return context.AuthTokens.FirstOrDefault(x => x.State == state)?.AccessToken ?? string.Empty;
        }
    }
}
