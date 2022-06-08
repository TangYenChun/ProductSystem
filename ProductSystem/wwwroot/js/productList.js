const API_BASE_URL = 'https://localhost:5001/api/ProductApi'

let app = new Vue({
    el: '#app',
    data: {
        isBusy: true,
        filter: null,
        filterOn: [],
        sortBy: '',
        sortDesc: false,
        sortDirection: 'asc',
        //sortDirection: 'asc',
        fields: [
            //欄位處理
            { key: 'productId', label: 'ID', class: 'text-center' },
            { key: 'productName', label: '產品名稱', class: 'text-center' },
            { key: 'categoryName', label: '種類', class: 'text-center' },
            { key: 'quantityPerUnit', label: '單位', class: 'text-center' },
            { key: 'unitPrice', label: '單價', class: 'text-center' },
            { key: 'unitsInStock', label: '庫存', class: 'text-center' },
            { key: 'discontinued', label: '是否停產', class: 'text-center' },
            { key: 'actions', label: '操作', class: 'text-center' }
        ],
        totalRows: 1,
        currentPage: 1,
        perPage: 5,
        pageOptions: [5, 10, 25, 100],
        productList: [],
        createProductData: {
            productName: '',
            categoryName: '',
            quantityPerUnit: '',
            unitPrice: '',
            unitsInStock: '',
            discontinued: ''
        },
        updateProductData: {
            productId: '',
            productName: '',
            categoryName: '',
            quantityPerUnit: '',
            unitPrice: '',
            unitsInStock: '',
            discontinued: ''
        },
        modalErrorMsg: '',
        currentProduct: {},
        uploadBusy: false
    },
    created() {
        this.getAllData()
    },
    methods: {
        onFiltered(filteredItems) {
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        getAllData() {
            axios.get(API_BASE_URL)
                .then((res) => {
                    if (res.status == 200) {
                        this.productList = res.data
                        console.log(res)
                        this.totalRows = this.productList.length
                        this.isBusy = false
                    }
                    else {
                        toastr.warning('資料讀取失敗')
                    }
                })
        },
        create(file) {
            this.uploadBusy = true
            const form = new FormData()
            form.append('file', file)

            axios.post(API_BASE_URL, form)
                .then((res) => {
                    if (res.data.status == 20000) {
                        axios.post(API_BASE_URL, this.createProductData)
                            .then((res) => {
                                if (res.data.status == 20000) {
                                    toastr.success('新增成功')
                                    this.getAllData()
                                    $('#create-modal').modal('hide')
                                    this.initData()
                                } else {
                                    toastr.warning('新增失敗')
                                    this.uploadBusy = false
                                    this.modalErrorMsg = res.data.msg
                                }
                            })
                            .catch((error) => { console.log(error) })

                    } else {
                        toastr.warning('新增失敗')
                        this.uploadBusy = false
                    }
                    this.uploadBusy = false
                })
                .catch((error) => { console.log(error) })
        },
        delete(product) {
            this.$bvModal.msgBoxConfirm('確定刪除資料?', {
                title: '警告',
                size: 'sm',
                okVariant: 'danger',
                okTitle: '確認',
                cancelTitle: '取消',
                footerClass: 'p-2',
                hideHeaderClose: false,
                centered: true
            })
                .then((value) => {
                    if (value) {
                        axios.delete(API_BASE_URL, { data: product.item })
                            .then((res) => {
                                if (res.data.status == 20000) {
                                    toastr.success('刪除成功')
                                    this.getAllData()
                                } else {
                                    toastr.warning('刪除失敗')
                                }
                            })
                            .catch((error) => { console.log(error) })
                    } else {
                        toastr.info('取消變更')
                    }
                })
                .catch((error) => { console.log(error) })
        },
        selectEdit(product) {
            let copyProduct = { ...product.item }
            this.currentProduct = copyProduct
        },
        update() {
            axios.put(API_BASE_URL, this.currentProduct)
                .then((res) => {
                    if (res.data.status == 20000) {
                        toastr.success('保存成功')
                        this.getAllData()
                        $('#edit-modal').modal('hide')
                    } else {
                        toastr.warning('保存失敗')
                        this.uploadBusy = false
                        this.modalErrorMsg = res.data.msg
                    }
                })
                .catch((error) => { console.log(error) })
        },
        initCreateData() {
            this.createProductData.productName = ''
            this.createProductData.categoryName = ''
            this.createProductData.quantityPerUnit = ''
            this.createProductData.unitPrice = ''
            this.createProductData.unitsInStock = ''
            this.createProductData.discontinued = ''
            this.modalErrorMsg = ''
        },
        initUpdateData() {
            this.modalErrorMsg = ''
        }
    }
})