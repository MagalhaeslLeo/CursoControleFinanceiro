﻿using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        private readonly Contexto _contexto;
        public RepositorioGenerico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Atualizar(TEntity entity)
        {
            try
            {
                var registro = _contexto.Set<TEntity>().Update(entity);
                registro.State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task Excluir(string id)
        {
            try
            {
                var entity = await PegarPeloID(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await PegarPeloID(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task Excluir(TEntity entity)
        {
            try
            {
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                await _contexto.AddAsync(entity);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task Inserir(List<TEntity> entity)
        {
            try
            {
                await _contexto.AddRangeAsync(entity);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task<TEntity> PegarPeloID(int id)
        {
            try
            {
                var entity = await _contexto.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task<TEntity> PegarPeloID(string id)
        {
            try
            {
                var entity = await _contexto.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public IQueryable<TEntity> PegarTodos()
        {
            try
            {
                return _contexto.Set<TEntity>();
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }
    }
}
