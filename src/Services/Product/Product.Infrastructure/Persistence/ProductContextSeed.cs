using Microsoft.AspNetCore.Identity;
using Polly;
using Product.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Product.Infrastructure.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedAsync(ProductContext context, ILogger logger)
        {
            try
            {
                await AddProductCategory(context);
                await context.SaveChangesAsync();
                await AddProduct(context);
                await context.SaveChangesAsync();
                logger.Information("Seeded data for Product DB associated with context ProductContext", nameof(ProductContext));
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                logger.Error("Seeded data for Product DB is failed !", nameof(ProductContext));
            }
        }

        public static async Task AddProduct(ProductContext context)
        {
            if (!context.Products.Any())
            {
                var query = context.ProductCategories.ToArray();
                var random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var product = new Domain.Entities.Product
                    {
                        CategoryID = query[random.Next(0, 9)].Id,  // Lựa chọn ngẫu nhiên một danh mục từ 1 đến 10
                        Name = "Product " + i,
                        Description = "Description " + i,
                        StarNumber = random.NextDouble() * 5.0,  // Điểm đánh giá ngẫu nhiên từ 0 đến 5
                        PromotionPrice = (decimal)(random.Next(500, 10000) / 100.0),  // Giá khuyến mãi ngẫu nhiên
                        Price = (decimal)(random.Next(1000, 5000) / 100.0),  // Giá ngẫu nhiên
                        Image = "product" + i + ".jpg",
                        ThumbImage = "product" + i + "-thumb.jpg",
                        ImageList = "product" + i + "-1.jpg,product" + i + "-2.jpg",
                        CreatedDate = DateTime.Now.AddDays(-random.Next(365)),  // Ngày tạo ngẫu nhiên trong vòng 1 năm
                        UpdatedDate = DateTime.Now.AddDays(-random.Next(365)),  // Ngày cập nhật ngẫu nhiên trong vòng 1 năm
                        UserCreated = 1,  // ID của người tạo ngẫu nhiên
                        UserUpdated = 1,  // ID của người cập nhật ngẫu nhiên
                        SeoAlias = "product-" + i,
                        SeoTitle = "Product " + i + " Title",
                        MetaKeywords = "product, sample, keywords",
                        MetaDescription = "Product " + i + " description",
                        Status = random.Next(1, 4),  // Trạng thái ngẫu nhiên từ 1 đến 3
                        Video = "product" + i + ".mp4",
                        Warranty = random.Next(1, 24),  // Thời gian bảo hành ngẫu nhiên từ 1 đến 24 tháng
                        ViewCount = random.Next(0, 1000)  // Số lượt xem ngẫu nhiên từ 0 đến 999
                    };
                    await context.Products.AddAsync(product);
                }

            }
        }

        public static async Task AddProductCategory(ProductContext context)
        {
            // Kiểm tra xem đã có dữ liệu trong bảng ProductCategory chưa
            if (!context.ProductCategories.Any())
            {
                for (int i = 0; i < 122; i++)
                {
                    var category = new ProductCategory
                    {
                        Name = "Category " + i,
                        Image = "",  // Thay đổi đường dẫn hình ảnh tại đây
                        SeoAlias = "category-" + i,
                        SeoTitle = "Category " + i + " Title",
                        MetaKeywords = "category, sample, keywords",
                        MetaDescription = "Category " + i + " description",
                        ParentID = i > 0 ? i - 1 : (int?)null,  // Gán ID danh mục cha ngẫu nhiên (nếu có)
                        SortOrder = i,
                        Visibility = true  // Tạo danh mục có hiển thị ngẫu nhiên
                    };

                    await context.ProductCategories.AddAsync(category);
                }
            }
        }
    }
}

