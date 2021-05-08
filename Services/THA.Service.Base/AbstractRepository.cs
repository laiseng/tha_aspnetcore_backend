using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using THA.Model.Base;
using THA.Model.Core;
using THA.Service.Base;

namespace THA.Service.Product
{
   public abstract class AbstractRepository<TEntity, TContext> : IRepositoryBase<TEntity>
       where TEntity : class, IBaseModel
       where TContext : DbContext
   {
      private readonly TContext context;
      private readonly IHttpContextAccessor httpContextAccessor;
      public AbstractRepository(TContext context, IHttpContextAccessor httpContextAccessor)
      {
         this.context = context;
         this.httpContextAccessor = httpContextAccessor;
      }

      /// <summary>
      /// Add entity to context
      /// </summary>
      /// <param name="entity">entity to add</param>
      /// <returns>returns added entity</returns>
      public async Task<TEntity> Add(TEntity entity)
      {
         try
         {
            if (entity == null) throw new ArgumentNullException($"{nameof(Update)} entity must not be null");

            var userId = int.Parse(this.httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);

            entity.CREATE_BY = userId;
            entity.EDIT_BY = userId;
            entity.CREATE_DATE = DateTime.UtcNow;
            entity.EDIT_DATE = DateTime.UtcNow;
            entity.STATUS = Statuses.NEW;

            context.Set<TEntity>().Add(entity);

            var count = await context.SaveChangesAsync();
            return count > 0 ? entity : null;
         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(Add)} Add failed with message - {ex.Message}");
         }
      }

      /// <summary>
      /// Delete entity by ID
      /// </summary>
      /// <param name="id">entity ID</param>
      /// <returns>returns deleted entity or null if none deleted</returns>
      public async Task<TEntity> Delete(int id)
      {
         try
         {
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
               return entity;
            }

            var userId = int.Parse(this.httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);

            entity.EDIT_BY = userId;
            entity.EDIT_DATE = DateTime.UtcNow;

            // No physical delete only set status to DELETE
            entity.STATUS = Statuses.DELETE;

            context.Set<TEntity>().Update(entity);

            var count = await context.SaveChangesAsync();
            return count > 0 ? entity : null;
         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(Delete)} Delete entityId: {id} failed with mesage - {ex.Message}");
         }
      }

      /// <summary>
      /// Get entity by ID
      /// </summary>
      /// <param name="id">entity ID</param>
      /// <returns>returns found entity or null when not found</returns>
      public async Task<TEntity> Get(int id)
      {
         try
         {
            // Deleted record is only by setting Statuses.DELETE, hence need to exclude it from find 
            var found = context.Set<TEntity>().Where(x => x.ID == id && x.STATUS != Statuses.DELETE);
            return await found.AnyAsync() ? await found.FirstAsync() : null;
         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(Get)} Get entityId: {id} failed with message - {ex.Message}");
         }
      }

      /// <summary>
      /// Get all entities
      /// </summary>
      /// <returns>returns all entities or null if none found</returns>
      public async Task<List<TEntity>> GetAll()
      {
         try
         {
            // Deleted record is only by setting Statuses.DELETE, hence need to exclude it from get all
            var found = context.Set<TEntity>().Where(x => x.STATUS != Statuses.DELETE);

            return await found.AnyAsync() ? await found.ToListAsync() : null;
         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(GetAll)} Get All entity failed with message - {ex.Message}");
         }
      }

      /// <summary>
      /// Update entity
      /// </summary>
      /// <param name="entity">update entity</param>
      /// <returns>return updated or null if none updated</returns>
      public virtual async Task<TEntity> Update(TEntity entity)
      {
         try
         {
            if (entity == null) throw new ArgumentNullException($"{nameof(Update)} entity must not be null");

            context.Entry(entity).State = EntityState.Modified;

            entity.EDIT_BY = int.Parse(this.httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);
            entity.EDIT_DATE = DateTime.UtcNow;
            entity.STATUS = Statuses.EDIT;

            context.Set<TEntity>().Update(entity);

            var count = await context.SaveChangesAsync();
            return count > 0 ? entity : null;
         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(Update)} Update entityId: {entity.ID} failed with message - {ex.Message}");
         }
      }

   }
}
