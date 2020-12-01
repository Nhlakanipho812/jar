using System;
using System.Collections.Generic;
using System.Linq;
using kinetic.Model;
using kinetic.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace kinetic.BusinessLogic
{
    public class CoinJar : ICoinJar
    {
        private readonly KineticDb _db;

        public CoinJar(KineticDb db)
        {
            _db = db;
        }

        public Results<string> AddCoin(ICoin coin)
        {
            try
            {
                using (_db)
                {
                    var jar = _db.Jars.ToList()[0];

                    //this will exit the code if a JAR is not found
                    if (jar == null)
                        return new Results<string>
                        {
                            Code = 400,
                            Errors = new List<string> {"We could not process your request, data is not available"},
                            Message = "We could not process your request, data is not available"
                        };

                    //check if the JAR is not full, if full exit code
                    if (coin.Volume > Convert.ToDecimal(jar.Volume))
                        return new Results<string>
                        {
                            Code = 400,
                            Errors = new List<string> {"The jar is full"},
                            Message =
                                $"Unfortunately you cannot add coins anymore, the jar is full. Please reset the jar. Volume available: {jar.Volume}"
                        };

                    //add coins into the jar
                    jar.Total += Convert.ToDouble(coin.Amount);
                    //remove volume from the jar
                    jar.Volume -= Convert.ToDouble(coin.Volume);

                    //this is a transaction history
                    var trans = new JarTransaction
                    {
                        Action = "added coins",
                        Amount = Convert.ToDouble(coin.Amount),
                        Volume = Convert.ToDouble(coin.Volume),
                        CreatedAt = DateTime.Now,
                        JarId = jar.Id
                    };

                    _db.Entry(jar).State = EntityState.Modified;
                    _db.JarTransactions.Add(trans);
                    _db.SaveChanges();
                    //if were successful, code should return OK
                    return new Results<string>
                    {
                        Code = 200,
                        Message = $"{coin.Amount} coins were added successfully to the Jar"
                    };
                }
            }
            catch (Exception e)
            {
                return new Results<string>
                {
                    Code = e.HResult,
                    Errors = new List<string> {e.Message, e.Source},
                    Message = e.Message
                };
            }
        }

        public Results<decimal> GetTotalAmount()
        {
            try
            {
                using (_db)
                {
                    var res = _db.Jars.ToList()[0];

                    if (res == null)
                        return new Results<decimal>
                        {
                            Code = 400,
                            Errors = new List<string> {"We could not process your request, data is not available"},
                            Message = "We could not process your request, data is not available"
                        };

                    return new Results<decimal>
                    {
                        Code = 200,
                        Message = "$" + Convert.ToDecimal(res.Total)
                    };
                }
            }
            catch (Exception e)
            {
                return new Results<decimal>
                {
                    Code = e.HResult,
                    Errors = new List<string> {e.Message, e.Source},
                    Message = e.Message
                };
            }
        }

        public Results<string> Reset()
        {
            try
            {
                using (_db)
                {
                    var jar = _db.Jars.ToList()[0];

                    //this will exit the code if a JAR is not found
                    if (jar == null)
                        return new Results<string>
                        {
                            Code = 400,
                            Errors = new List<string> {"We could not process your request, data is not available"},
                            Message = "We could not process your request, data is not available"
                        };

                    //set total coins to 0, update record
                    jar.Total = 0;
                    jar.Volume = 42;
                    _db.Entry(jar).State = EntityState.Modified;
                    _db.SaveChanges();

                    //if were successful, code should return OK
                    return new Results<string>
                    {
                        Code = 200,
                        Message = "Data was reset successfully"
                    };
                }
            }
            catch (Exception e)
            {
                return new Results<string>
                {
                    Code = e.HResult,
                    Errors = new List<string> {e.Message, e.Source},
                    Message = e.Message
                };
            }
        }

        public JarTransactionViewModel GetTransactions()
        {
            try
            {
                using (_db)
                {
                    var trans = _db.JarTransactions.ToList().Select(s => new DataView
                    {
                        Action = s.Action,
                        Amount = s.Amount,
                        Volume = s.Volume,
                        CreatedAt = s.CreatedAt,
                        JarId = s.JarId,
                        Id = s.Id
                    }).ToList();
                    return new JarTransactionViewModel
                    {
                        Code = 200,
                        Data = trans,
                        Errors = null,
                        Message = "Successfully Loaded Transactions"
                    };
                }
            }
            catch (Exception e)
            {
                return new JarTransactionViewModel
                {
                    Code = e.HResult,
                    Data = null,
                    Errors = new List<string> {e.Message, e.Source},
                    Message = e.Message
                };
            }
        }
    }

    public interface ICoinJar
    {
        Results<string> AddCoin(ICoin coin);
        Results<decimal> GetTotalAmount();
        Results<string> Reset();
        JarTransactionViewModel GetTransactions();
    }
}