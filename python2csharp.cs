namespace Namespace {
    
    using datetime = datetime.datetime;
    
    using System.Collections.Generic;
    
    public static class Module {
        
        public class Product {
            
            public Product(object productId, object price, object name) {
                this._productId = productId;
                this._price = price;
                this._name = name;
            }
        }
        
        public class Receipt {
            
            public Receipt(object cashNo) {
                this._cashNo = cashNo;
                this._rows = new List<object>();
                this.date = datetime.now();
            }
            
            public virtual object Total() {
                var sum = 0;
                foreach (var row in this._rows) {
                    sum += row._perPrice * row._count;
                }
                return sum;
            }
            
            public virtual object AddRow(object productId, object perPrice, object count, object productName) {
                foreach (var row in this._rows) {
                    if (row.GetProductId() == productId) {
                        row.AddCount(count);
                        return;
                    }
                }
                this._rows.append(ReceiptRow(productId, perPrice, count, productName));
            }
        }
        
        public class ReceiptRow {
            
            public ReceiptRow(object productId, object perPrice, object count, object productName) {
                this._productId = productId;
                this._perPrice = perPrice;
                this._count = count;
                this._productName = productName;
            }
            
            public virtual object GetProductId() {
                return this._productId;
            }
            
            public virtual object AddCount(object count) {
                this._count += count;
            }
        }
        
        public static object r = Receipt("1");
        
        static Module() {
            r.AddRow("123", 12, 2, "banan");
            r.AddRow("123", 12, 1, "banan");
        }
    }
}
