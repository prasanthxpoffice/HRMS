window.dashboardCharts = {
    renderChart: function (containerId, optionsJson) {
        try {
            const options = JSON.parse(optionsJson);
            const container = document.getElementById(containerId);
            if (!container) return;
            
            // Clear existing
            container.innerHTML = '';
            
            const chart = new ApexCharts(container, options);
            chart.render();
            
            return true;
        } catch (e) {
            console.error('Chart Render Error:', e);
            return false;
        }
    }
};
