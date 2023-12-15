﻿using AutoMapper;
using Ecommerce_Business.Repository.IRepository;
using Ecommerce_Common;
using Ecommerce_DataAccess;
using Ecommerce_DataAccess.Data;
using Ecommerce_DataAccess.ViewModels;
using Ecommerce_Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(objDTO);
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();

                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                    // method 1
                    // _db.OrderDetails.Add(details); 

                }
                //method 2
                _db.OrderDetails.AddRange(obj.OrderDetails);
                await _db.SaveChangesAsync();

                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()

                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objDTO;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (objHeader == null)
            {
                IEnumerable<OrderDetail> objDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == id);

                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }

            return 0; // if header is null we retrun 0
        }

        public async Task<OrderDTO> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id)
            };

            if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }

            return new OrderDTO(); //if null create new OrderDTO
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id)
                };

                OrderFromDb.Add(order);

            }

            //do some filtering  #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDTO();
            }

            if (data.Status == SD.Status_Pending)
            {
                data.Status = SD.Status_Confirmed;
                await _db.SaveChangesAsync();

                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }

            return new OrderHeaderDTO();

        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
        {
            if (objDTO != null)
            {
                var OrderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(objDTO);
                _db.OrderHeaders.Update(OrderHeader);
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(OrderHeader);
            }

            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }

            data.Status = status;
            if (status == SD.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _db.SaveChangesAsync();

            return true;

        }
    }
}