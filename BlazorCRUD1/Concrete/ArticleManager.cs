﻿using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCRUD1.Concrete
{
    public class ArticleManager : IArticleManager
    {
        private readonly IDapperManager _dapperManager;

        public ArticleManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Article article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Title", article.Title, DbType.String);
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<Article> GetById(int id)
        {
            var article = Task.FromResult(_dapperManager.Get<Article>($"select * from [Article] where ID = {id}", null,
                    commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [Article] where ID = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Article] WHERE Title like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<Article>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var articles = Task.FromResult(_dapperManager.GetAll<Article>
                ($"SELECT * FROM [Article] WHERE Title like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(Article article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", article.ID);
            dbPara.Add("Title", article.Title, DbType.String);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
