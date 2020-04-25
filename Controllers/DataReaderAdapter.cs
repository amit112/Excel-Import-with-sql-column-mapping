namespace ExcelImportWithColSelectionByAmit.Controllers {
    internal class DataReaderAdapter<T> {
        private object customers;

        public DataReaderAdapter(object customers)
        {
            this.customers = customers;
        }
    }
}